using CompanyEmployees;
using CompanyEmployees.Extensions;
using CompanyEmployees.Presentation.ActionFilters;
using CompanyEmployees.utility;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Service;
using Repository;
using Service.Contracts;
using Service.DataShaping;
using Shared.DataTransferObjects;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

var configPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.config");
LogManager.Setup().LoadConfigurationFromFile(configPath);// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//builder.Services.AddControllers()
builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    //config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    var jsonPatchFormatter = GetJsonPatchInputFormatter();
    if (jsonPatchFormatter != null)
    {
        config.InputFormatters.Insert(0, jsonPatchFormatter);
    }
    //config.CacheProfiles.Add("120SecondsDuration",
    //   new CacheProfile { Duration = 120});
}).AddXmlDataContractSerializerFormatters()
  .AddCustomCSVFormatter()
  .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);


builder.Services.AddCustomMediaTypes();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureOutputCaching();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ConfigureSwagger();

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
{
    var formatter = new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>()
        .FirstOrDefault();

    return formatter;
}

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();
builder.Services.AddScoped<IEmployeeLinks, EmployeeLinks>();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

if (app.Environment.IsProduction())
    app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders =
    ForwardedHeaders.All });
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Maze API v1");
    s.SwaggerEndpoint("/swagger/v2/swagger.json", "Code Maze API v2");
});
//app.UseRouting();
app.UseRateLimiter();
app.UseCors("CorsPolicy");
//app.UseResponseCaching();
app.UseOutputCache();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.Run();



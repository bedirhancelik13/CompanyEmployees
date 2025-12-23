using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Hosting.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class MigrationManager
    {
        private static int _numberOfRetries;

        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RepositoryContext>();

            try
            {
                context.Database.Migrate();
            }
            catch
            {
                if (_numberOfRetries < 6)
                {
                    _numberOfRetries++;
                    Thread.Sleep(10000);
                    Console.WriteLine($"SQL Server hazır değil. Tekrar deneme #{_numberOfRetries}");
                    return app.MigrateDatabase();
                }

                throw;
            }

            return app;
        }
    }
}

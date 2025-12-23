#FROM mcr.microsoft.com/dotnet/sdk:8.0
#WORKDIR /home/app
#COPY . .
#RUN dotnet restore
#RUN dotnet publish ./src/CompanyEmployees/CompanyEmployees.csproj -o /publish/
#WORKDIR /publish
#ENV ASPNETCORE_URLS=http://+:5000
#ENV SECRET=CodeMazeSecretKey123456789!!!!!!
#ENTRYPOINT ["dotnet", "CompanyEmployees.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-image

WORKDIR /home/app

# Tüm çözümü içeri kopyala
COPY . .

# 1) NuGet paketlerini indir
RUN dotnet restore

# 2) Unit testleri çalıştır
RUN dotnet test ./tests/Tests/Tests.csproj

# 3) Ana Web API projesini publish et
RUN dotnet publish ./src/CompanyEmployees/CompanyEmployees.csproj -o /publish/


# --- RUNTIME İMAJI (hafif olan kısım) ---

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /publish

# Build aşamasından sadece publish çıktısını al
COPY --from=build-image /publish .

# Uygulamanın dinleyeceği adres
ENV ASPNETCORE_URLS=http://+:5000

# Kitaptaki SECRET örneği
ENV SECRET=CodeMazeSecretKey123456789!!!!!!

# Uygulamayı çalıştır
ENTRYPOINT ["dotnet", "CompanyEmployees.dll"]


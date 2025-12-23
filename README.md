# CompanyEmployees – ASP.NET Core Web API

Bu proje, **ASP.NET Core Web API** kullanılarak geliştirilmiş, **clean architecture** prensiplerini temel alan, **Docker** ve **MSSQL** entegrasyonuna sahip, modern ve RESTful bir backend uygulamasıdır.

Proje; veri şekillendirme (data shaping), HATEOAS, JWT tabanlı kimlik doğrulama, filtreleme, sayfalama ve Docker Compose gibi ileri seviye backend kavramlarını içermektedir.

---

## 🚀 Kullanılan Teknolojiler

- **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core**
- **Microsoft SQL Server**
- **Docker & Docker Compose**
- **JWT Authentication**
- **HATEOAS**
- **AutoMapper**
- **NLog**
- **Swagger / OpenAPI**

---

## 🧱 Mimari Yapı

Proje **Clean Architecture** yaklaşımıyla katmanlı olarak tasarlanmıştır:

- **Presentation** → API Controller’lar
- **Service** → İş kuralları (Business Logic)
- **Repository** → Veri erişim katmanı
- **Entities** → Domain modelleri
- **Shared** → DTO’lar ve ortak yapılar

Bu yapı sayesinde:
- Kod okunabilirliği artar
- Test edilebilirlik kolaylaşır
- Katmanlar birbirinden bağımsız hale gelir

---

## 🔐 Özellikler

- ✅ JWT tabanlı Authentication & Authorization
- ✅ Data Shaping (fields parametresi ile alan seçimi)
- ✅ HATEOAS desteği (hypermedia links)
- ✅ Sayfalama (Paging), Filtreleme ve Sıralama
- ✅ Rate Limiting & Output Caching
- ✅ Global Exception Handling
- ✅ Swagger UI üzerinden test edilebilir API

---

## 🐳 Docker ile Çalıştırma

### Gereksinimler
- Docker
- Docker Compose

### 1️⃣ Projeyi klonla
```bash
git clone https://github.com/KULLANICI_ADIN/CompanyEmployees.git
cd CompanyEmployees
```
### 2️⃣ Docker Compose ile ayağa kaldır
```bash
docker-compose up --build
```
🌐 API Erişim Noktaları
-https://localhost:8081/swagger

🧪 HATEOAS Kullanımı
HATEOAS destekli cevap almak için Accept header eklenmelidir:
-Accept: application/vnd.codemaze.hateoas+json
Alan seçimi için:
-GET /api/companies/{companyId}/employees?fields=name,age

📦 Veritabanı
-SQL Server
-Docker Compose ile otomatik kurulum
-Migration’lar uygulama başlatılırken otomatik çalışır

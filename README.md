### Run

dotnet restore
dotnet ef database update --project Blogs.Data --startup-project Blogs.API
dotnet run --project Blogs.API

### Swagger
https://localhost:7081/swagger

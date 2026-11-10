dotnet ef migrations add InitialCreate --output-dir Data/Migrations
dotnet ef database update    








{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Reservas;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}



using Microsoft.EntityFrameworkCore;
using Parcial.Entities;


//public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    
}








dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.5 








builder.Services.AddScoped<CelularService>();
builder.Services.AddScoped<CelularRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));




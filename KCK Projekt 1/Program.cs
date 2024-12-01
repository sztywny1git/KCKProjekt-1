using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Dodaj usługi do kontenera DI.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Utwórz aplikację.
var app = builder.Build();

// Skonfiguruj aplikację.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Konfiguracja CORS
app.UseCors("AllowReactApp");

// Konfiguracja routingu i kontrolerów.
app.UseRouting();
app.MapControllers(); // Automatycznie mapuje kontrolery bez użycia `UseEndpoints`.

// Uruchomienie aplikacji na porcie 5000.
app.Run("http://localhost:5000");

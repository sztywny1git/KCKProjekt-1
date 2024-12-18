﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Dodaj usługi do kontenera DI.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<UzytkownikModel>();
builder.Services.AddScoped<ProduktModel>();
builder.Services.AddScoped<Koszyk>();


// Utwórz aplikację.
var app = builder.Build();

// Skonfiguruj aplikację.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Konfiguracja CORS
app.UseCors("AllowAll");

// Konfiguracja routingu i kontrolerów.
app.UseRouting();
app.MapControllers(); // Automatycznie mapuje kontrolery bez użycia `UseEndpoints`.

// Uruchomienie aplikacji na porcie 5000.
app.Run("http://localhost:5000");

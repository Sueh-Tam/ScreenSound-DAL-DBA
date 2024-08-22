using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Endpoints;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSound_OFC.Banco;
using ScreenSound_OFC.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
            policy =>
            {
                policy.WithOrigins("http://localhost:7137", "http://localhost:7048/Swagger/index.html",
                    "\"https://localhost:7048/Swagger/index.html\"", "https://localhost:7137")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();
app.UseStaticFiles();

app.AddEndPointsArtistas();
app.AddEndPointsMusicas();
app.AddEndPointGeneros();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("MyPolicy");
app.Run();

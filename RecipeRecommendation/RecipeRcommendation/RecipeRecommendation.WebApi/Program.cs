using RecipeRecommendation.Domain.Interfaces;
using RecipeRecommendation.Application.Services;
using DotnetGeminiSDK.Client;
using DotnetGeminiSDK;
using RecipeRecommendation.Infrastructure.Services;
using static System.Net.WebRequestMethods;
using RecipeRecommendation.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .WithHeaders("Content-Type","Accept");
        });
});

builder.Services.AddScoped<IGeminiAIService, GeminiAIService>();
builder.Services.AddScoped<IRecipeRecommendationService, RecommedationService>();
builder.Services.Configure<GoogleApiSettings>(builder.Configuration.GetSection("GoogleApiSettings"));
var config = builder.Configuration.GetSection("GoogleApiSettings").Get<GoogleApiSettings>();
Console.WriteLine($"Loaded API Key: {config?.ApiKey}");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI;

var builder = WebApplication.CreateBuilder(args);

//Add DB context to DI using the connection string stored in User Secrets
builder.Services.AddDbContext<AppDbContext>
    (opt => opt.UseSqlServer(builder.Configuration["SQLDbConnection"]));

//add repo to DI
builder.Services.AddScoped<IPokemonRepo, SQLPokemonRepo>();
//add automapper to DI
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseHttpsRedirection();

//map endpoints
app.ConfigureApi();

app.Run();
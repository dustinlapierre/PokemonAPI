using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;

var builder = WebApplication.CreateBuilder(args);

//Add DB context to DI using the connection string stored in User Secrets
builder.Services.AddDbContext<AppDbContext>
    (opt => opt.UseSqlServer(builder.Configuration["SQLDbConnection"]));

//add repo to DI
builder.Services.AddScoped<IPokemonRepo, SQLPokemonRepo>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("api/v1/pokemon", async (IPokemonRepo repo) =>
{
    var pokemon = await repo.GetAllPokemonAsync();

    return Results.Ok(pokemon);
});

app.MapGet("api/v1/pokemon/{id}", async (IPokemonRepo repo, int id) =>
{
    var pokemon = await repo.GetPokemonAsync(id);
    if (pokemon == null)
        return Results.NotFound();

    return Results.Ok(pokemon);
});

app.Run();
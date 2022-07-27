using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Models;

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

app.MapPost("api/v1/pokemon", async (IPokemonRepo repo, Pokemon pokemon) =>
{
    await repo.CreatePokemonAsync(pokemon);
    return Results.Created($"api/v1/pokemon/{pokemon.Id}", pokemon);
});

//id of resource to update, object holding the update data
app.MapPut("api/v1/pokemon/{id}", async (IPokemonRepo repo, int id, Pokemon pokemon) =>
{
    var pokemonModel = await repo.GetPokemonAsync(id);
    if (pokemonModel == null)
        return Results.NotFound();

    pokemonModel.Name = pokemon.Name;
    pokemonModel.PokedexNumber = pokemon.PokedexNumber;
    pokemonModel.Generation = pokemon.Generation;

    await repo.UpdatePokemonAsync(id, pokemonModel);
    return Results.NoContent();
});

app.MapDelete("api/v1/pokemon/{id}", async (IPokemonRepo repo, int id) =>
{
    var pokemonModel = await repo.GetPokemonAsync(id);
    if (pokemonModel == null)
        return Results.NotFound();

    await repo.DeletePokemonAsync(pokemonModel);
    return Results.NoContent();
});

app.Run();
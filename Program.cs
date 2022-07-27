using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Models;
using PokemonAPI.DTOs;
using AutoMapper;

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

app.MapGet("api/v1/pokemon", async (IPokemonRepo repo, IMapper mapper) =>
{
    var pokemon = await repo.GetAllPokemonAsync();

    return Results.Ok(mapper.Map<List<PokemonDTO>>(pokemon));
});

app.MapGet("api/v1/pokemon/{id}", async (IPokemonRepo repo, int id, IMapper mapper) =>
{
    var pokemonModel = await repo.GetPokemonAsync(id);
    if (pokemonModel == null)
        return Results.NotFound();

    //DTO mapping Map<Destination Type>(Source Object)
    var pokemonDTO = mapper.Map<PokemonDTO>(pokemonModel);

    return Results.Ok(pokemonDTO);
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
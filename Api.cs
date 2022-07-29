using AutoMapper;
using PokemonAPI.Data;
using PokemonAPI.DTOs;
using PokemonAPI.Models;

namespace PokemonAPI;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("api/v1/pokemon", GetAllPokemon);

        app.MapGet("api/v1/pokemon/{id}", async (IPokemonRepo repo, int id, IMapper mapper) =>
        {
            var pokemonModel = await repo.GetPokemonAsync(id);
            if (pokemonModel == null)
                return Results.NotFound();

            //DTO mapping Map<Destination Type>(Source Object)
            var pokemonDTO = mapper.Map<PokemonReadDTO>(pokemonModel);

            return Results.Ok(pokemonDTO);
        });

        app.MapPost("api/v1/pokemon", async (IPokemonRepo repo, PokemonCreateDTO pokemonCreateDTO, IMapper mapper) =>
        {
            var pokemonModel = mapper.Map<Pokemon>(pokemonCreateDTO);
            await repo.CreatePokemonAsync(pokemonModel);
            return Results.Created($"api/v1/pokemon/{pokemonModel.Id}", mapper.Map<PokemonReadDTO>(pokemonModel));
        });

        //id of resource to update, object holding the update data
        app.MapPut("api/v1/pokemon/{id}", async (IPokemonRepo repo, int id, PokemonUpdateDTO pokemonUpdateDTO, IMapper mapper) =>
        {
            var pokemonModel = await repo.GetPokemonAsync(id);
            if (pokemonModel == null)
                return Results.NotFound();

            mapper.Map(pokemonUpdateDTO, pokemonModel);

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
    }

    public static async Task<IResult> GetAllPokemon(IPokemonRepo repo, IMapper mapper)
    {
        try
        {
            var pokemon = await repo.GetAllPokemonAsync();

            return Results.Ok(mapper.Map<List<PokemonReadDTO>>(pokemon));
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }
}
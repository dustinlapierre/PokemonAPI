using AutoMapper;
using PokemonAPI.Data;
using PokemonAPI.DTOs;
using PokemonAPI.Models;

namespace PokemonAPI;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        //GET
        app.MapGet("api/v1/pokemon", GetAllPokemon);
        app.MapGet("api/v1/pokemon/{id}", GetPokemon);

        //POST
        app.MapPost("api/v1/pokemon", CreatePokemon);

        //PUT
        app.MapPut("api/v1/pokemon/{id}", UpdatePokemon);

        //DELETE
        app.MapDelete("api/v1/pokemon/{id}", DeletePokemon);
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

    public static async Task<IResult> GetPokemon(IPokemonRepo repo, int id, IMapper mapper)
    {
        try
        {
            var pokemonModel = await repo.GetPokemonAsync(id);
            if (pokemonModel == null)
                return Results.NotFound();

            //DTO mapping Map<Destination Type>(Source Object)
            var pokemonDTO = mapper.Map<PokemonReadDTO>(pokemonModel);

            return Results.Ok(pokemonDTO);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    public static async Task<IResult> CreatePokemon(IPokemonRepo repo, PokemonCreateDTO pokemonCreateDTO, IMapper mapper)
    {
        try
        {
            var pokemonModel = mapper.Map<Pokemon>(pokemonCreateDTO);
            await repo.CreatePokemonAsync(pokemonModel);
            return Results.Created($"api/v1/pokemon/{pokemonModel.Id}", mapper.Map<PokemonReadDTO>(pokemonModel));
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    public static async Task<IResult> UpdatePokemon(IPokemonRepo repo, int id, PokemonUpdateDTO pokemonUpdateDTO, IMapper mapper)
    {
        try
        {
            var pokemonModel = await repo.GetPokemonAsync(id);
            if (pokemonModel == null)
                return Results.NotFound();

            mapper.Map(pokemonUpdateDTO, pokemonModel);

            await repo.UpdatePokemonAsync(id, pokemonModel);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    public static async Task<IResult> DeletePokemon(IPokemonRepo repo, int id)
    {
        try
        {
            var pokemonModel = await repo.GetPokemonAsync(id);
            if (pokemonModel == null)
                return Results.NotFound();

            await repo.DeletePokemonAsync(pokemonModel);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }
}
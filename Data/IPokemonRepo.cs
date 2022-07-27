using PokemonAPI.Models;

namespace PokemonAPI.Data
{
    public interface IPokemonRepo
    {
        Task<List<Pokemon>> GetAllPokemonAsync();
        Task<Pokemon?> GetPokemonAsync(int id);
        Task CreatePokemonAsync(Pokemon pokemon);
    }
}
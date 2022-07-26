using PokemonAPI.Models;

namespace PokemonAPI.Data
{
    public interface IPokemonRepo
    {
        Task<List<Pokemon>> GetAllPokemonAsync();
    }
}
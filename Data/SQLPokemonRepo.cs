using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Data
{
    public class SQLPokemonRepo : IPokemonRepo
    {
        private readonly AppDbContext _context;
        public SQLPokemonRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Pokemon>> GetAllPokemonAsync()
        {
            return await _context.Pokemon.ToListAsync();
        }
    }
}
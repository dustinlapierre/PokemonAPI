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

        public async Task CreatePokemonAsync(Pokemon pokemon)
        {
            await _context.Pokemon.AddAsync(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemon.Remove(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Pokemon>> GetAllPokemonAsync()
        {
            return await _context.Pokemon.ToListAsync();
        }

        public async Task<Pokemon?> GetPokemonAsync(int id)
        {
            return await _context.Pokemon.FindAsync(id);
        }

        public async Task UpdatePokemonAsync(int id, Pokemon pokemon)
        {
            //with entity Framework the model should have already been updated, and just need to be saved
            await _context.SaveChangesAsync();
        }
    }
}
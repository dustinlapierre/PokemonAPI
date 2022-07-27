using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.DTOs
{
    //DTO parent
    public abstract class PokemonBaseDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int PokedexNumber { get; set; }
        [Required]
        public string? Generation { get; set; }
    }
}
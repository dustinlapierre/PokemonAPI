using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.DTOs
{
    public class PokemonUpdateDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int PokedexNumber { get; set; }
        [Required]
        public string? Generation { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.DTOs
{
    //DTO used when creating data
    public class PokemonCreateDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int PokedexNumber { get; set; }
        [Required]
        public string? Generation { get; set; }
    }
}
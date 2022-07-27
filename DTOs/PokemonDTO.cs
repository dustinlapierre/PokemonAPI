using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.DTOs
{
    public class PokemonDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int PokedexNumber { get; set; }
    }
}
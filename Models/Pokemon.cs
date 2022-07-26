using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int PokedexNumber { get; set; }
        [Required]
        public string? Generation { get; set; }
    }
}
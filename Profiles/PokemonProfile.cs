using AutoMapper;
using PokemonAPI.DTOs;
using PokemonAPI.Models;

namespace PokemonAPI.Profiles
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            // Source -> Destination
            CreateMap<Pokemon, PokemonReadDTO>();
            CreateMap<PokemonCreateDTO, Pokemon>();
        }
    }
}
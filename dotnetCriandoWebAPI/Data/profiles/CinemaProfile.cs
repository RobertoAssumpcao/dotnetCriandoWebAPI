using AutoMapper;
using dotnetCriandoWebAPI.Models;
using dotnetCriandoWebAPI.Models.DTO;

namespace dotnetCriandoWebAPI.Data.profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<AdicionarCinemaRequest, Cinema>();
        CreateMap<Cinema, GetCinemaResponse>();
    }
}
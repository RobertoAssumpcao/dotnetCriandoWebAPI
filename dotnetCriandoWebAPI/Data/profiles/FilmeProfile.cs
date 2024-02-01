using AutoMapper;
using dotnetCriandoWebAPI.Models;
using dotnetCriandoWebAPI.Models.DTO;

namespace dotnetCriandoWebAPI.Data.profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<AdicionarFilmeRequest, Filme>();
        CreateMap<Filme, GetFilmeResponse>();
    }
}
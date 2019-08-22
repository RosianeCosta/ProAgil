using AutoMapper;
using ProAgil.WebAPI.Dtos;
using ProAgil.Domain;
using System.Linq;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
       public AutoMapperProfiles(){
        CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Palestrantes, opt => {
                    opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Palestrante).ToList());
                })
                .ReverseMap();

          CreateMap<EventoDto, Evento>();

         CreateMap<Palestrante, PalestranteDto>()
                .ForMember(dest => dest.Eventos, opt => {
                    opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Evento).ToList());
                })
                .ReverseMap();

         CreateMap<Palestrante, PalestranteDto>();
         CreateMap<PalestranteDto, Palestrante>();  

         CreateMap<Lote, LoteDto>();
         CreateMap<LoteDto, Lote>();
         
         CreateMap<RedeSocial, RedeSocialDto>();
         CreateMap<RedeSocialDto, RedeSocial>();
       }
    }
}
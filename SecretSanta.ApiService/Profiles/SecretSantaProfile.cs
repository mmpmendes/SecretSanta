using AutoMapper;

using SecretSanta.ApiService.DTOs;
using SecretSanta.Models.Models;

namespace SecretSanta.ApiService.Profiles;

public class SecretSantaProfile : Profile
{
    public SecretSantaProfile()
    {
        CreateMap<ParAmigoDTO, DrawEntry>()
            .ForMember(dest => dest.GiverName, opt => opt.MapFrom(src => src.Dador.Nome))
            .ForMember(dest => dest.GiverEmail, opt => opt.MapFrom(src => src.Dador.Email))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Recebedor.Nome))
            .ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.Recebedor.Email))
            .ForMember(dest => dest.GiverId, opt => opt.Ignore()) // Adjust based on your needs
            .ForMember(dest => dest.ReceiverId, opt => opt.Ignore()).ReverseMap(); // Adjust based on your needs
    }
}

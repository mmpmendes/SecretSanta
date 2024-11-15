using AutoMapper;

using SecretSanta.ApiService.DTOs;
using SecretSanta.Models.Models;

namespace SecretSanta.ApiService.Profiles;

public class SecretSantaProfile : Profile
{
    public SecretSantaProfile()
    {
        CreateMap<PairFriendDTO, DrawEntry>()
            .ForMember(dest => dest.GiverName, opt => opt.MapFrom(src => src.Giver.Name))
            .ForMember(dest => dest.GiverEmail, opt => opt.MapFrom(src => src.Giver.Email))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.Name))
            .ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.Receiver.Email))
            .ForMember(dest => dest.GiverId, opt => opt.Ignore()) // Adjust based on your needs
            .ForMember(dest => dest.ReceiverId, opt => opt.Ignore()).ReverseMap(); // Adjust based on your needs
    }
}

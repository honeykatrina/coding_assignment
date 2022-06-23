using AutoMapper;
using UserAccountManagement.Models.Entities;
using UserAccountManagement.Models.Requests;
using UserAccountManagement.Models.Responses;

namespace UserAccountManagement.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseModel>()
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Account.Balance))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Account.CustomerId));

        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src));

        CreateMap<CreateUserRequest, Account>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialCredit));
    }
}
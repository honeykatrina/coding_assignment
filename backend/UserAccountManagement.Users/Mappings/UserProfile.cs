using AutoMapper;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Users.Models.Entities;
using UserAccountManagement.Users.Models.Responses;

namespace UserAccountManagement.Users.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<(int, double), CreateAccountRequest>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Item1))
            .ForMember(dest => dest.InitialCredit, opt => opt.MapFrom(src => src.Item2));

        CreateMap<User, UserResponseModel>()
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Accounts.Sum(x => x.Balance)));

        CreateMap<CreateAccountRequest, Account>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialCredit));

        CreateMap<Account, AccountResponseModel>();
    }
}
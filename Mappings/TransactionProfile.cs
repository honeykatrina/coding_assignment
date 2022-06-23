using AutoMapper;
using UserAccountManagement.Models.Entities;
using UserAccountManagement.Models.Requests;
using UserAccountManagement.Models.Responses;

namespace UserAccountManagement.Mappings;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<(CreateUserRequest, Guid), Transaction>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Item1.InitialCredit))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Item2))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));

        CreateMap<Transaction, TransactionResponseModel>();
    }
}
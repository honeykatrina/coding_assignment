using AutoMapper;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.TransactionModule.Models.Responses;
using UserAccountManagement.TransactionModule.Models.Entities;

namespace UserAccountManagement.TransactionModule.Mappings;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<CreateTransaction, Transaction>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));

        CreateMap<(CreateUserRequest, Guid), CreateTransaction>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Item1.InitialCredit))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Item2));

        CreateMap<Transaction, TransactionResponseModel>();
    }
}
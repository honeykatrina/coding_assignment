using AutoMapper;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Transactions.Models.Responses;
using UserAccountManagement.Transactions.Models.Entities;

namespace UserAccountManagement.Transactions.Mappings;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<CreateTransaction, Transaction>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));

        CreateMap<(CreateAccountRequest, Guid), CreateTransaction>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Item1.InitialCredit))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Item2));

        CreateMap<Transaction, TransactionResponseModel>();
    }
}
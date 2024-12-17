using AutoMapper;
using expenses_api.DTOs.Transaction;
using expenses_api.DTOs.User;
using expenses_api.Models;

namespace expenses_api.Utils;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionCreateDto, Transaction>();
        CreateMap<TransactionUpdateDto, Transaction>();
    }
}
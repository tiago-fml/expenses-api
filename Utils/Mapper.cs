using AutoMapper;
using expenses_api.DTOs.Transaction;
using expenses_api.DTOs.User;
using expenses_api.Models;

namespace expenses_api.Utils;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<User, UserDTO>();
        CreateMap<User, UserAuthDTO>();
        CreateMap<UserCreateDTO, User>();
        CreateMap<UserUpdateDTO, User>();
        
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionCreateDto, Transaction>();
        CreateMap<TransactionUpdateDto, Transaction>();
    }
}
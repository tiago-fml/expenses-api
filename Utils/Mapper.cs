using AutoMapper;
using expenses_api.DTOs.Category;
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
        
        CreateMap<Transaction, TransactionDTO>();
        CreateMap<TransactionCreateDTO, Transaction>();
        CreateMap<TransactionUpdateDTO, Transaction>();
        
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<CategoryUpdateDTO, Category>();
    }
}
using AutoMapper;
using ExpenseManager.Data;
using ExpenseManager.Models.Category;
using ExpenseManager.Models.Transaction;
using ExpenseManager.Models.Transaction_Type;

namespace ExpenseManager.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        { 
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<TransactionType, GetTransactionTypeDto>().ReverseMap();
            CreateMap<TransactionType, TransactionTypeDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using Project_NGO.Models;
using Project_NGO.Requests.Cash;
using Project_NGO.Requests.Categories;

namespace Project_NGO.Requests.Program;

public class ProgramProfile:Profile
{
    public ProgramProfile()
    {
        CreateMap<Programs, ProgramDTO>();
        CreateMap<ProgramDTO, Programs>();
        CreateMap<Receipt, ReceiptDTO>();
        CreateMap<ReceiptDTO, Receipt>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<Category, CategoryDTO > ();
    }
}
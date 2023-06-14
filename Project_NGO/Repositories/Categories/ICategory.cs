using Project_NGO.Models;
using Project_NGO.Requests.Categories;

namespace Project_NGO.Repositories.Categories
{
    public interface ICategory
    {
        Task<IEnumerable<Category>>GetCategoriesAsync();
        Task<Category>GetCategoryByIdAsync(int id);
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto, IFormFile photo);
        Task<Category> UpdateCategoryAsync(CategoryDTO categoryDto,int id, IFormFile? photo);
        Task<bool> DeleteCategoryAsync(int id);
    }
}

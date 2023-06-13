using Project_NGO.Models;

namespace Project_NGO.Repositories.Categories
{
    public interface ICategory
    {
        Task<IEnumerable<Category>>GetCategoriesAsync();
        Task<Category>GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category, IFormFile photo);
        Task<Category> UpdateCategoryAsync(Category category, IFormFile? photo);
        Task<bool> DeleteCategoryAsync(int id);
    }
}

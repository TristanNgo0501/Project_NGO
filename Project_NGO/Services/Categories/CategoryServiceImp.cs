

using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories.Categories;

namespace Project_NGO.Services.Categories
{
    public class CategoryServiceImp : ICategory
    {
        private readonly DatabaseContext _dbContext;
        private readonly string _uploadFolder;

        public CategoryServiceImp(DatabaseContext dbContext, IWebHostEnvironment webHostEnvironment )
        {
            _dbContext = dbContext;
            _uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath,"Upload");
        }

        public async Task<Category> AddCategoryAsync(Category category,IFormFile photo)
        {
           if (photo!= null && photo.Length>0) {
            string fileName= Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string filePath= Path.Combine(_uploadFolder,fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                category.Image = "/Upload/" + fileName;
            }
           await _dbContext.Categories.AddAsync(category);
           await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            if (category != null)
            {
                if(!string.IsNullOrEmpty(category.Image))
                { 
                    string filePath= Path.Combine(_uploadFolder,category.Image);
                    if(File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

    
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(Category category, IFormFile? photo)
        {
            var cateDb = await _dbContext.Categories.FindAsync(category.Id);
            if(photo != null && photo.Length>0)
            {
                if (!string.IsNullOrEmpty(cateDb.Image))
                {
                    string filePathUpdate = Path.Combine(_uploadFolder,cateDb.Image);
                    if (File.Exists(filePathUpdate))
                    {
                        File.Delete(filePathUpdate);
                    }
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                string filePath = Path.Combine(_uploadFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                category.Image = "/Upload/" + fileName;
                _dbContext.Entry(category).State= EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return category;
            }
            else
            {
                category.Image = cateDb.Image;
                _dbContext.Entry(category).State= EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return category;
            }

            }
        }
    }


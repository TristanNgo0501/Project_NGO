

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories.Categories;
using Project_NGO.Repositories.UploadFileRepo;
using Project_NGO.Requests.Categories;

namespace Project_NGO.Services.Categories
{
    public class CategoryServiceImp : ICategory
    {
        private readonly DatabaseContext _dbContext;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        public CategoryServiceImp(DatabaseContext dbContext, IFileRepository fileRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto,IFormFile photo)
        {
            var cate = _mapper.Map<Category>(categoryDto);
           if (photo!= null && photo.Length>0) {
                var fileName = await _fileRepository.UploadFile(photo, "Category");
                cate.Image = "http://localhost:5065/Category/" + fileName;
            }
           await _dbContext.Categories.AddAsync(cate);
           await _dbContext.SaveChangesAsync();
            var cateDto = _mapper.Map<CategoryDTO>(cate);
            return cateDto;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            if (category != null)
            {
                await _fileRepository.DeleteFile(category.Image);
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

        public async Task<Category> UpdateCategoryAsync(CategoryDTO categoryDto,int id, IFormFile? photo)
        {
            var cate = _mapper.Map<Category>(categoryDto);
            cate.Id = id;
            var cateDb = await _dbContext.Categories.FindAsync(cate.Id);
            if(photo != null && photo.Length>0)
            {
                await _fileRepository.DeleteFile(cateDb.Image);
                var fileName = await _fileRepository.UploadFile(photo, "Category");
                cate.Image = "http://localhost:5065/Category/" + fileName;
            }
            else
            {
                cate.Image = cateDb.Image;
            }
            _dbContext.Entry(cate).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            //var updatedCate = _mapper.Map<CategoryDTO>(cate);
            return cate;

        }
        }
    }


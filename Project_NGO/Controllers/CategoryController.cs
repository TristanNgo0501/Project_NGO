using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models;
using Project_NGO.Repositories.Categories;
using Project_NGO.Requests.Categories;
using Project_NGO.Utils;
using System.Resources;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepository;

        public CategoryController(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var resources = await _categoryRepository.GetCategoriesAsync();
                if (resources != null && resources.Any())
                {
                    var response = new CustomStatusResult<IEnumerable<Category>>
                        (StatusCodes.Status200OK, "Get list category successfully", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<IEnumerable<Category>>
                        (StatusCodes.Status404NotFound, "Not found result or result empty", null, null);
                    return NotFound(response);
                }
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomStatusResult<Category>()
                {
                    Message = "An error occured while retrived model",
                    Error = new List<string> { ex.Message }
                }) ;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var resource = await _categoryRepository.GetCategoryByIdAsync(id);
                if (resource == null)
                {
                    var response = new CustomStatusResult<Category>(404, "Resource not found", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomStatusResult<Category>(200, "Get category successfully", resource, null);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomStatusResult<Category>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = new List<string> { ex.Message }
                });
            }
           
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory([FromForm] CategoryDTO categoryDto, IFormFile? photo)
        {
            try
            {
                var resource = await _categoryRepository.AddCategoryAsync(categoryDto, photo);
                if(resource != null)
                {
                    var response = new CustomStatusResult<CategoryDTO>(StatusCodes.Status201Created,"Resource created", resource, null);
                    return Ok(response);
                }
                else
                {
                    var reponse= new CustomStatusResult<Category>(StatusCodes.Status400BadRequest,"Unable to create resource",null,null);
                    return BadRequest(reponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory([FromForm] CategoryDTO categoryDto,int id, IFormFile? photo)
         {
            try
            {
                    var resource = await _categoryRepository.UpdateCategoryAsync(categoryDto,id, photo);
                if (resource != null)
                {
                    var response = new CustomStatusResult<Category>(StatusCodes.Status200OK, "update category successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<Category>(StatusCodes.Status400BadRequest, "No category to update", resource, null) ;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(int id)
        {
            bool resourceDeleted = false;
            var resource  = await _categoryRepository.GetCategoryByIdAsync (id);
            if(resource != null)
            {
                resourceDeleted= await _categoryRepository.DeleteCategoryAsync (id);
            }
            if (resourceDeleted)
            {
                var response = new CustomStatusResult<string>(200,
                    "Resource deleted successfully", null, null);
                    return Ok(response);

            }
            else
            {
                var response = new CustomStatusResult<string>(404,
                    "Resource not found or unable to delete", null, null);
                return NotFound(response);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories.NewFolder;
using Project_NGO.Utils;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {
        private readonly IContactForm _contactForm;
        public ContactFormController(IContactForm contactForm)
        {
            _contactForm = contactForm;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact_Form>>> GetContact_Forms()
        {
            try
            {
                var resources = await _contactForm.GetContact_FormsAsync();
                if (resources != null && resources.Any())
                {
                    var response = new CustomStatusResult<IEnumerable<Contact_Form>>
                        (StatusCodes.Status200OK, "Get list contact form successfully", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<IEnumerable<Contact_Form>>
                        (StatusCodes.Status404NotFound, "Not found result or result empty", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomStatusResult<Contact_Form>()
                {
                    Message = "An error occured while retrived model",
                    Error = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Contact_Form>> AddContact_Form([FromForm] Contact_Form contactform)
        {
            try
            {
                var resource = await _contactForm.AddContact_FormAsync(contactform);
                if (resource != null)
                {
                    var response = new CustomStatusResult<Contact_Form>(201, "Resource created", contactform, null);
                    return CreatedAtAction(nameof(GetContactForm), new { id = contactform.Id }, contactform);
                }
                else
                {
                    var reponse = new CustomStatusResult<Contact_Form>(400, "Unable to create resource", null, null);
                    return BadRequest(reponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomStatusResult<Contact_Form>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact_Form>> GetContactForm(int id)
        {
            try
            {
                var resource = await _contactForm.GetContact_FormByIdAsync(id);
                if (resource == null)
                {
                    var response = new CustomStatusResult<Contact_Form>(404, "Resource not found", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomStatusResult<Contact_Form>(200, "Get contact form successfully", resource, null);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomStatusResult<Contact_Form>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = new List<string> { ex.Message }
                });
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contact_Form>> UpdateContact_Form([FromForm] Contact_Form contact_form)
        {
            try
            {
                var resource = await _contactForm.GetContact_FormByIdAsync(contact_form.Id);
                if (resource != null)
                {
                    var resourceUpdate = await _contactForm.UpdateContact_FormsAsync(contact_form);
                    var response = new CustomStatusResult<Contact_Form>(200, "update contact form successfully", resourceUpdate, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<Contact_Form>(404, "No contact form to update", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomStatusResult<Contact_Form>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = new List<string> { ex.Message }
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactFormReadStatus(int id)
        {
            try
            {
                var updatedContactForm = await _contactForm.UpdateContactFormReadStatusAsync(id);
                if (updatedContactForm != null)
                {
                 
                    var response = new CustomStatusResult<Contact_Form>(200, "update contact form successfully", updatedContactForm, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<Contact_Form>(404, "No contact form to update", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomStatusResult<Contact_Form>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = new List<string> { ex.Message }
                });
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(int id)
        {
            bool resourceDeleted = false;
            var resource = await _contactForm.GetContact_FormByIdAsync(id);
            if (resource != null)
            {
                resourceDeleted = await _contactForm.DeleteContact_FormsAsync(id);
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

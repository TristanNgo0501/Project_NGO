using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories.Categories;
using Project_NGO.Repositories.NewFolder;

namespace Project_NGO.Services.ContactForm
{
    public class ContactFormServiceImp : IContactForm
    {
        private readonly DatabaseContext _dbContext;

        public ContactFormServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contact_Form> AddContact_FormAsync(Contact_Form contactform)
        {
            contactform.CreatedAt = DateTime.Now;
            await _dbContext.Contact_Forms.AddAsync(contactform);
            await _dbContext.SaveChangesAsync();
            return contactform;
        }

        public async Task<bool> DeleteContact_FormsAsync(int id)
        {

            var contacform = await GetContact_FormByIdAsync(id);
            if (contacform != null)
            {
                _dbContext.Contact_Forms.Remove(contacform);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Contact_Form> GetContact_FormByIdAsync(int id)
        {
            return await _dbContext.Contact_Forms.FindAsync(id);
        }

        public async Task<IEnumerable<Contact_Form>> GetContact_FormsAsync()
        {
            return await _dbContext.Contact_Forms.ToListAsync();
        }

        public async Task<Contact_Form> UpdateContactFormReadStatusAsync(int id)
        {
            var contactForm = await GetContact_FormByIdAsync(id);
            if (contactForm != null)
            {
                contactForm.IsRead = true;
                _dbContext.Contact_Forms.Update(contactForm);
                await _dbContext.SaveChangesAsync();
                return contactForm;
            }
            return null;

            
        }

        public async Task<Contact_Form> UpdateContact_FormsAsync(Contact_Form contactform)
            {
                var contactDb = await _dbContext.Contact_Forms.FindAsync(contactform.Id);
                if (contactDb != null)
                {
                    contactDb.Name = contactform.Name;
                    contactDb.Address = contactform.Address;
                    contactDb.Mobilephone = contactform.Mobilephone;
                    contactDb.Email = contactform.Email;
                    contactDb.Message = contactform.Message;
                    contactDb.IsRead = contactform.IsRead;
                await _dbContext.SaveChangesAsync();
                }

                return contactDb;
            }
        

    } 
}
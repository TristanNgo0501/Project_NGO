using Microsoft.AspNetCore.Mvc;
using Project_NGO.Data;
using Project_NGO.Models;

namespace Project_NGO.Repositories.NewFolder
{
    public interface IContactForm
    {
        Task<IEnumerable<Contact_Form>> GetContact_FormsAsync();
        Task<Contact_Form> GetContact_FormByIdAsync(int id);
        Task<Contact_Form> AddContact_FormAsync(Contact_Form contactform);
        Task<Contact_Form> UpdateContact_FormsAsync(Contact_Form contactform);
        Task<bool> DeleteContact_FormsAsync(int id);
        Task<Contact_Form> UpdateContactFormReadStatusAsync(int id);
    }
}

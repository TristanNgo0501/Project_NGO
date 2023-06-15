using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Models
{
    public class Contact_Form
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string? Address { get; set; }
        [Required(ErrorMessage = "Mobile phone is required.")]
      //  [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid mobile phone number.")]
        public string Mobilephone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
     //   [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public DateTime? CreatedAt { get;set; }
        
        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}


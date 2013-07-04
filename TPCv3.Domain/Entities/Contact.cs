using System.ComponentModel.DataAnnotations;

namespace TPCv3.Domain.Entities{
    public class Contact{
        [Required]
        public string Name { get; set; }


        [Required, EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [Url]
        public string Website { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
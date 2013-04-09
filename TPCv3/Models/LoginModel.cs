using System.ComponentModel.DataAnnotations;

namespace TPCv3.Models{
    public class LoginModel{
        #region Public Properties

        [Required(ErrorMessage = "Username is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Username { get; set; }

        #endregion
    }
}
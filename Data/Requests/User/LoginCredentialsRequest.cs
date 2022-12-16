using System.ComponentModel.DataAnnotations;

namespace RadioAPI.Data.Requests.User
{
    public class LoginCredentialsRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]

        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required]
        [StringLength(255, ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
    }
}

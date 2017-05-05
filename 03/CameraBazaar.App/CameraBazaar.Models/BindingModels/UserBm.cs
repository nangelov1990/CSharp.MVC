namespace CameraBazaar.Models.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UserBm
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Phone]
        [RegularExpression("^\\+[\\d]{10,12}$")]
        public string Phone { get; set; }
    }
}

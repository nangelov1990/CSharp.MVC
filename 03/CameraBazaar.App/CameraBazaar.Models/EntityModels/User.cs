namespace CameraBazaar.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(20)]
        [DisplayFormat(ConvertEmptyStringToNull = false, NullDisplayText = "[Not Specified]")]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

        [Phone]
        [RegularExpression("^\\+[\\d]{10,12}$")]
        public string Phone { get; set; }

        public virtual IEnumerable<Camera> Cameras { get; set; }
    }
}

namespace Dashboard.Models.BindingModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Account;

    public class RegisterCustomerBindingModel : RegisterViewModel
    {
        [Required, MinLength(2), MaxLength(30)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required, MinLength(2), MaxLength(30)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
    }
}
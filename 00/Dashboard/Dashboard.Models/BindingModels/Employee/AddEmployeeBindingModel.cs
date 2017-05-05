namespace Dashboard.Models.BindingModels.Employee
{
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Account;

    public class AddEmployeeBindingModel : RegisterViewModel
    {
        [Required, MinLength(2), MaxLength(30)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}

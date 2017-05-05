namespace Dashboard.Models.BindingModels.Request
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class AddRequestBindingModel
    {
        [Required]
        public RequestType Type { get; set; }

        [Required]
		[MinLength(2), MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(10), MaxLength(500)]
        public string Message { get; set; }
    }
}

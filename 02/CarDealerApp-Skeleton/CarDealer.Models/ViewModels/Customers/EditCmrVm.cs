namespace CarDealer.Models.ViewModels.Customers
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditCmrVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("DOB")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
    }
}
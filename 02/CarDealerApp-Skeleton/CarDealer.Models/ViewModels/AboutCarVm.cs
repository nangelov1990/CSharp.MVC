namespace CarDealer.Models.ViewModels
{
    using System.Collections.Generic;

    public class AboutCarVm
    {
        public CarVm Car { get; set; }

        public IEnumerable<PartVm> Parts { get; set; }
    }
}
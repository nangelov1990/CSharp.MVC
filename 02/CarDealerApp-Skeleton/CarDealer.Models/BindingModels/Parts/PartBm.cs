﻿namespace CarDealer.Models.BindingModels.Parts
{
    public class PartBm
    {
        public string Name { get; set; }

        public double? Price { get; set; }

        public int Quantity { get; set; }

        public int SupplierId { get; set; }
    }
}
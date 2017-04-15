namespace CarDealer.Models.ViewModels.Sales
{
    using System;
    using Cars;
    using Customers;

    public class AddSaleReviewVm
    {
        private double? carPrice;
        private double discount;

        public AddSaleCustomerVm Customer { get; set; }
        public AddSaleCarVm Car { get; set; }

        public double Discount
        {
            get
            {
                if (Customer.IsYoungDriver)
                {
                    return this.discount <= 95
                        ? this.discount + 5
                        : 100;
                }
                else
                {
                    return this.discount;
                }
            }
            set { this.discount = value; }
        }

        public string DiscountString
        {
            get
            {
                if (Customer.IsYoungDriver)
                {
                    return this.discount <= 95
                        ? $"{this.discount}% (+5%)"
                        : "100% (+5%)";
                }
                else
                {
                    return $"{this.discount}%";
                }
            }
        }

        public double CarPrice
        {
            get { return this.carPrice ?? 0; }
            set { this.carPrice = value; }
        }

        public double FinalCarPrice
        {
            get
            {
                var calculatedDiscount = this.Discount / 100;
                var price = this.CarPrice - (this.CarPrice * calculatedDiscount);
                price = Math.Round(price, 2);

                return price;
            }
        }
    }
}
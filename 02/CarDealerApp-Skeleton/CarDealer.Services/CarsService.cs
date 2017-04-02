namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Models.EntityModels;
    using Models.ViewModels;

    public class CarsService : Service
    {
        public IEnumerable<AllCarsByMakeVm> GetCarsByMake(string make)
        {
            IEnumerable<Car> cars;
            if (make == null)
            {
                cars = this.Context.Cars
                    .OrderBy(c => c.Model)
                    .ThenBy(c => c.TravelledDistance);
            }
            else
            {
                cars = this.Context.Cars
                    .Where(c => c.Make == make)
                    .OrderBy(c => c.Model)
                    .ThenBy(c => c.TravelledDistance);
            }

            IEnumerable<AllCarsByMakeVm> viewModels =
                Mapper.Instance.Map<IEnumerable<Car>, IEnumerable<AllCarsByMakeVm>>(cars);

            return viewModels;
        }

        public AboutCarVm GetCarWithParts(int id)
        {
            Car car = this.Context.Cars.Find(id);
            IEnumerable<Part> parts = car.Parts;

            CarVm carVm = Mapper.Instance.Map<Car, CarVm>(car);
            IEnumerable<PartVm> partsVm = Mapper.Instance.Map<IEnumerable<Part>, IEnumerable<PartVm>>(parts);

            AboutCarVm viewModel = new AboutCarVm()
            {
                Car = carVm,
                Parts = partsVm
            };

            return viewModel;
        }
    }
}
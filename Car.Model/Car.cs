using Car.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Model
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public string CarNumber { get; set; }
        public CarType Type { get; set; }
        public double EngineCapacity { get; set; }
        public string Color { get; set; }
        public double DailyFare { get; set; }
        public bool HasDriver { get; set; }

        public ICollection<DriverCar> DriverCars { get; set; }
        public ICollection<RentedCar> RentedCars { get; set; }
    }
}

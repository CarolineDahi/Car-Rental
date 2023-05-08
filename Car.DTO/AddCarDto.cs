using Car.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DTO
{
    public class AddCarDto
    {
        public string CarNumber { get; set; }
        public CarType Type { get; set; }
        public double EngineCapacity { get; set; }
        public string Color { get; set; }
        public double DailyFare { get; set; }
        public bool HasDriver { get; set; }
    }
}

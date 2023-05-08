using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Model
{
    public class RentedCar 
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public Guid RenderId { get; set; }
        public Renter Renter { get; set; } 
    }
}

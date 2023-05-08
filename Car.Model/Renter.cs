using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Model
{
    public class Renter
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string NationalID { get; set; }

        public ICollection<RentedCar> RentedCars { get; set; }
    }
}

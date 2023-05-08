using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Model
{
    public class Driver
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public ICollection<DriverCar> Cars { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DTO
{
    public class GetCarDto : AddCarDto
    {
        public Guid Id { get; set; }
    }
}

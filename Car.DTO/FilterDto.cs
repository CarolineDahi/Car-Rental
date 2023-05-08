using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DTO
{
    public class FilterDto
    {
        public int PageIndex {  get; set; }
        public int PageSize { get; set; }
        public string? SearchString { get; set; }
        public string? SortBy { get; set; }
    }
}

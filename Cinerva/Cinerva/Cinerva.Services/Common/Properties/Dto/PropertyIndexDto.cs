using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Properties.Dto
{
    public class PropertyIndexDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int TotalRooms { get; set; }
        public int NumberOfDaysForRefunds { get; set; }
        public string CityName { get; set; }
        public string AdministratorName { get; set; }
        public string PropetyTypeName { get; set; }
    }
}

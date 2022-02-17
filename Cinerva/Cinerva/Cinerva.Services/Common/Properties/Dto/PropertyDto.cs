using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Properties.Dto
{
    public class PropertyDto: PropertyIndexDto
    {
        public int CityId { get; set; }
        public int AdministratorId { get; set; }
        public int PropetyTypeId { get; set; }
    }
}

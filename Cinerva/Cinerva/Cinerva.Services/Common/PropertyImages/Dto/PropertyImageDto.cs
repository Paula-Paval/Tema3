using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.PropertyImages.Dto
{
    public class PropertyImageDto
    {

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int PropetyId { get; set; }
    }
}

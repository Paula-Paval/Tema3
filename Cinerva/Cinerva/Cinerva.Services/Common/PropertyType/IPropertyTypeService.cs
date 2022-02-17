using Cinerva.Services.Common.PropertyType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.PropertyType
{
    public  interface IPropertyTypeService
    {
        public IList<PropertyTypeDto> GetPropertyTypes();
    }
}

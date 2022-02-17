using Cinerva.Services.Common.Properties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Properties
{
    public interface IPropertyService
    {
        public IList<PropertyIndexDto> GetProperties();
        public void CreateProperty(PropertyDto propertyDto);
        public PropertyIndexDto GetProperty(int id);
        public void UpdateProperty(PropertyDto property);
        public void DeleteProperty(PropertyIndexDto property);
        public PropertyDto GetPropertyWithForeignKeys(int id);
        public int GetLastId();
    }
}

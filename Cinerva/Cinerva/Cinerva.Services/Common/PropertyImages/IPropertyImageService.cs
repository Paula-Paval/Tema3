using Cinerva.Services.Common.PropertyImages.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.PropertyImages
{
    public interface IPropertyImageService
    {
        public void CreatePropertyImage(PropertyImageDto propertyImageDto);
        public IList<string> GetPropertyImageUrlByPropertyId(int id);
        public void DeletePropertyImageByPropertyId(int id);
    }
}

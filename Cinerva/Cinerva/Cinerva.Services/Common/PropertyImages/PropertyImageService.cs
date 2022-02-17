using Cinerva.Services.Common.PropertyImages.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinerva.Data;
using Cinerva.Data.Entities;

namespace Cinerva.Services.Common.PropertyImages
{
    public class PropertyImageService : IPropertyImageService
    {

        private readonly CinervaDbContext dbContext;
        public PropertyImageService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreatePropertyImage(PropertyImageDto propertyImageDto)
        {
            if (propertyImageDto == null) throw new ArgumentNullException(nameof(propertyImageDto));

            var propertyImageEntity = new PropertyImage
            {
                ImageUrl = propertyImageDto.ImageUrl,
                PropetyId = propertyImageDto.PropetyId,

            };

            dbContext.PropertyImages.Add(propertyImageEntity);
            dbContext.SaveChanges();
        }

        public IList<string> GetPropertyImageUrlByPropertyId(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var propertyImageEntities = dbContext.PropertyImages.Where(p => p.PropetyId == id).ToList();

            return propertyImageEntities.Select(x=>x.ImageUrl).ToList();

        }

        public void  DeletePropertyImageByPropertyId(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var propertyImageEntities = dbContext.PropertyImages.Where(p => p.PropetyId == id).ToList();

            foreach(var entity in propertyImageEntities)
            {
                dbContext.PropertyImages.Remove(entity);
               
            }

            dbContext.SaveChanges();
        }

    }
}



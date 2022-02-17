using Cinerva.Data;
using Cinerva.Services.Common.PropertyType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.PropertyType
{
    public class PropertyTypeService:IPropertyTypeService
    {
        private readonly CinervaDbContext dbContext;
        public PropertyTypeService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<PropertyTypeDto> GetPropertyTypes()
        {
            return dbContext.PropertyTypes.Select(x => new PropertyTypeDto
            {
                Id = x.Id,
                Type=x.Type

            }).ToList();
        }

    }
}

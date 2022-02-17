using Cinerva.Data;
using Cinerva.Data.Entities;
using Cinerva.Services.Common.Properties.Dto;
using Cinerva.Services.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Cinerva.Services.Common.Properties
{
    public class PropertyService:IPropertyService
    {
        private readonly CinervaDbContext dbContext;
        public PropertyService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PropertyIndexDto GetProperty(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var propertyEntity = dbContext.Properties.Include(p => p.User).Include(p => p.PropertyType).Include(p => p.City)
                .Where(p => p.Id == id)
                .SingleOrDefault();

            if (propertyEntity == null) return null;

            return new PropertyIndexDto
            {
                Id = propertyEntity.Id,
                Name = propertyEntity.Name,
                Address = propertyEntity.Address,
                AdministratorName = propertyEntity.User.FirstName+' '+propertyEntity.User.LastName,
                CityName = propertyEntity.City.Name,
                Description = propertyEntity.Description,
                NumberOfDaysForRefunds = propertyEntity.NumberOfDaysForRefunds,
                Phone = propertyEntity.Phone,
                PropetyTypeName = propertyEntity.PropertyType.Type,
                Rating = propertyEntity.Rating,
                TotalRooms = propertyEntity.TotalRooms
            };
        }


        public IList<PropertyIndexDto>GetProperties()
        {
            return dbContext.Properties.Select(x => new PropertyIndexDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                AdministratorName = x.User.FirstName + ' ' + x.User.LastName,
                CityName = x.City.Name,
                Description = x.Description,
                NumberOfDaysForRefunds = x.NumberOfDaysForRefunds,
                Phone = x.Phone,
                PropetyTypeName = x.PropertyType.Type,
                Rating = x.Rating,
                TotalRooms = x.TotalRooms
            }).ToList();
        }

        public void CreateProperty(PropertyDto propertyDto)
        {
            if (propertyDto == null) throw new ArgumentNullException(nameof(propertyDto));

            var propertyEntity = new Property
            {
                Name = propertyDto.Name,
                Address = propertyDto.Address,
               AdministratorId = propertyDto.AdministratorId,
               CityId = propertyDto.CityId,
                Description = propertyDto.Description,
                NumberOfDaysForRefunds = propertyDto.NumberOfDaysForRefunds,
                Phone = propertyDto.Phone,
                PropetyTypeId = propertyDto.PropetyTypeId,
                Rating = propertyDto.Rating,
                TotalRooms = propertyDto.TotalRooms

            };

            dbContext.Properties.Add(propertyEntity);
            dbContext.SaveChanges();
        }

        public PropertyDto GetPropertyWithForeignKeys(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var propertyEntity = dbContext.Properties.Where(p => p.Id == id).SingleOrDefault();

            if (propertyEntity == null) return null;

            return new PropertyDto
            {
                Id = propertyEntity.Id,
                Name = propertyEntity.Name,
                Address = propertyEntity.Address,
                AdministratorId = propertyEntity.AdministratorId,
                CityId = propertyEntity.CityId,
                Description = propertyEntity.Description,
                NumberOfDaysForRefunds = propertyEntity.NumberOfDaysForRefunds,
                Phone = propertyEntity.Phone,
                PropetyTypeId = propertyEntity.PropetyTypeId,
                Rating = propertyEntity.Rating,
                TotalRooms = propertyEntity.TotalRooms

            };
        }
        public void UpdateProperty(PropertyDto property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var propertyEntity = dbContext.Properties.Find(property.Id);

            if (propertyEntity == null) throw new EntityNotFoundException();

            propertyEntity.Name = property.Name;
            propertyEntity.Address = property.Address;
            propertyEntity.AdministratorId = property.AdministratorId;
            propertyEntity.CityId = property.CityId;
            propertyEntity.Description = property.Description;
            propertyEntity.NumberOfDaysForRefunds = property.NumberOfDaysForRefunds;
            propertyEntity.Phone = property.Phone;
            propertyEntity.PropetyTypeId = property.PropetyTypeId;
            propertyEntity.Rating = property.Rating;
            propertyEntity.TotalRooms = property.TotalRooms;

            dbContext.SaveChanges();
        }

        public void DeleteProperty(PropertyIndexDto property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var propertyEntity = dbContext.Properties.Find(property.Id);

            if (propertyEntity == null) throw new EntityNotFoundException();

            dbContext.Properties.Remove(propertyEntity);
            dbContext.SaveChanges();
        }

       public int GetLastId()
       {
            return dbContext.Properties.OrderBy(p=>p.Id).Last().Id;
       }
    }
}

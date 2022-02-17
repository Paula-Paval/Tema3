using Cinerva.Data;
using Cinerva.Services.Common.Cities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Cities
{
    public class CityService:ICityService
    {
        private readonly CinervaDbContext dbContext;

        public CityService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<CityDto> GetCities()
        {
            return dbContext.Cities.Select(x => new CityDto
            {
                Id = x.Id,
                Name = x.Name,
                CountryId=x.CountryId

            }).ToList();
        }

        public IList<CityDto> GetPage(int currentPage, int numberOfElementsPerPage)
        {
            return dbContext.Cities.Skip((currentPage - 1) * numberOfElementsPerPage)
           .Take(numberOfElementsPerPage)
           .Select(x => new CityDto
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Name = x.Name

            }).ToList();
           
        }
        public int GetCount()
        {
            return dbContext.Cities.Count();
        }
    }
}

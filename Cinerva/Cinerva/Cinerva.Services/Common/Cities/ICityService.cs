using Cinerva.Services.Common.Cities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Cities
{
    public interface ICityService
    {
        public IList<CityDto> GetCities();
        public IList<CityDto> GetPage(int currentPage, int numberOfElementsPerPage);
        public int GetCount();  
    }
}

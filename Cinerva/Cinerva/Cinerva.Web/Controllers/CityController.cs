using Cinerva.Services.Common.Cities;
using Cinerva.Services.Common.Cities.Dto;
using Cinerva.Web.ActionFilters;
using Cinerva.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Cinerva.Web.Controllers
{
    
    public class CityController : BaseController
    {
        private readonly ICityService cityService;

        public CityController( ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public IActionResult Index(int? pageNumber )
        {
            var numberOfElementsPerPage = 5;

            int page = (pageNumber ?? 1);

            var numberOfElements = cityService.GetCount();

            var cityDtos = cityService.GetPage(page, numberOfElementsPerPage);   
            
            var cityViewModels = cityDtos.Select(x => GetCityViewModelFromDto(x));

            var totalPages= (int)Math.Ceiling((decimal)numberOfElements/ numberOfElementsPerPage);

            this.ViewBag.Page = page;
            this.ViewBag.TotalPages = totalPages;        
            
            return View(cityViewModels);

        }

        #region[PRIVATE METHODS]
        private static CityViewModel GetCityViewModelFromDto(CityDto cityDto)
        {
            if (cityDto == null) return null;

            return new CityViewModel
            {
                Id = cityDto.Id,
                Name = cityDto.Name,
                CountryId=cityDto.CountryId
               
            };
        }
        #endregion
    }
}

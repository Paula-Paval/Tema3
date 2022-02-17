using Azure.Storage.Blobs;
using Cinerva.Services.Common.Cities;
using Cinerva.Services.Common.Properties;
using Cinerva.Services.Common.Properties.Dto;
using Cinerva.Services.Common.PropertyImages;
using Cinerva.Services.Common.PropertyImages.Dto;
using Cinerva.Services.Common.PropertyType;
using Cinerva.Services.Common.Users;
using Cinerva.Web.ActionFilters;
using Cinerva.Web.Blob;
using Cinerva.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Web.Controllers
{

    public class PropertyController : BaseController
    {

        private readonly IPropertyService propertyService;
        private readonly ICityService cityService;
        private readonly IUserService userService;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IPropertyImageService propertyImageSerive;
        private readonly IBlobService blobService;

      

        public PropertyController(IPropertyService propertyService, ICityService cityService, IUserService userService, IPropertyTypeService propertyTypeService, IPropertyImageService propertyImageService, IBlobService blobService)
        {
            this.propertyService = propertyService;
            this.cityService = cityService;
            this.userService = userService;
            this.propertyTypeService= propertyTypeService;
            this.propertyImageSerive= propertyImageService;
            this.blobService= blobService;
        }

        [HttpGet]
        public IActionResult Index(int? pageNumber)
        {
            int page = (pageNumber ?? 1);

            var propertiesDtos = propertyService.GetProperties();
            var propertiesViewModels = propertiesDtos.Select(x => GetPropertyViewModelFromDto(x));

            return View(propertiesViewModels.ToPagedList(page, 5));
        }



        [HttpGet] 
        public IActionResult Create()
        {
            var property = new PropertyCreateViewModel();

            var cities= GetCities(null).ToList();
            property.ListOfCities = cities;

            var admins = GetAdmins(null).ToList();
            property.ListOfAdministrators = admins;

            var types= GetPropertyTypes(null).ToList();
            property.ListOfPropertyTypes = types;
            return View(property);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PropertyCreateViewModel propertyViewModel)
        {
            if(!ModelState.IsValid)
            {
                propertyViewModel.ListOfCities = GetCities(propertyViewModel.CityId).ToList();
                propertyViewModel.ListOfAdministrators = GetAdmins(propertyViewModel.AdministratorId).ToList();
                propertyViewModel.ListOfPropertyTypes = GetPropertyTypes(propertyViewModel.PropetyTypeId).ToList();

                return View(propertyViewModel);
            }

            var propertyDto = GetPropertyDtoFromViewModel(propertyViewModel);

            propertyService.CreateProperty(propertyDto);

            await CreatePropertyImages(propertyViewModel);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1) return RedirectToAction("Index");

            var propertyDto = propertyService.GetPropertyWithForeignKeys(id);
            if (propertyDto == null) return RedirectToAction("Index");

            var propertyViewModel = GetPropertyEditViewModelFromDto(propertyDto);

            return View(propertyViewModel);

        }


        [HttpPost]
        public IActionResult Edit(PropertyEditViewModel propertyViewModel)
        {

            if (!ModelState.IsValid)
            {
                propertyViewModel.ListOfCities= GetCities(propertyViewModel.CityId).ToList();
                propertyViewModel.ListOfAdministrators=GetAdmins(propertyViewModel.AdministratorId).ToList();
                propertyViewModel.ListOfPropertyTypes=GetPropertyTypes(propertyViewModel.PropetyTypeId).ToList();   
                return View(propertyViewModel);
            }

            var propertyDto = propertyService.GetPropertyWithForeignKeys(propertyViewModel.Id);
            if(propertyDto==null) return RedirectToAction("Index");

            var propertyDtoUpdated=GetPropertyDtoFromViewModel(propertyViewModel);
            propertyService.UpdateProperty(propertyDtoUpdated);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Details(int id)
        {

            if (id < 1) return RedirectToAction("Index");

            var propertyDto = propertyService.GetProperty(id);
            if (propertyDto == null) return RedirectToAction("Index");

            var propertyViewModel = GetPropertyViewModelFromDto(propertyDto);
            var images=propertyImageSerive.GetPropertyImageUrlByPropertyId(id);
            propertyViewModel.ListOfImages = images.ToList();

            return View(propertyViewModel);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id < 1) return RedirectToAction("Index");

            var propertyDto = propertyService.GetProperty(id);
            if (propertyDto == null) return RedirectToAction("Index");

            var propertyViewModel = GetPropertyViewModelFromDto(propertyDto);
            
            return View(propertyViewModel);

        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var propertyDto = propertyService.GetProperty(id);
            if (propertyDto == null) return RedirectToAction("Index");

            await DeleteImagesFromBlob(id);

            propertyImageSerive.DeletePropertyImageByPropertyId(id);

            propertyService.DeleteProperty(propertyDto);

            return RedirectToAction("Index");
        }



        #region[PRIVATE METHODS]
        private static PropertyViewModel GetPropertyViewModelFromDto(PropertyIndexDto propertyDto)
        {
            if (propertyDto == null) return null;

            return new PropertyViewModel
            {
                Id = propertyDto.Id,
                Name = propertyDto.Name,
                Address = propertyDto.Address,
                AdministratorName = propertyDto.AdministratorName,
                CityName = propertyDto.CityName,
                Description = propertyDto.Description,
                NumberOfDaysForRefunds = propertyDto.NumberOfDaysForRefunds,
                Phone = propertyDto.Phone,
                PropetyTypeName = propertyDto.PropetyTypeName,
                Rating = propertyDto.Rating,
                TotalRooms = propertyDto.TotalRooms
            };

        }

        private  PropertyEditViewModel GetPropertyEditViewModelFromDto(PropertyDto propertyDto)
        {
            if (propertyDto == null) return null;

            var propertyViewModel=new PropertyEditViewModel
            {
                Id = propertyDto.Id,
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

            var cities = GetCities(propertyViewModel.CityId).ToList();
            propertyViewModel.ListOfCities = cities;

            var admins = GetAdmins(propertyViewModel.AdministratorId).ToList();
            propertyViewModel.ListOfAdministrators = admins;

            var types = GetPropertyTypes(propertyViewModel.PropetyTypeId).ToList();
            propertyViewModel.ListOfPropertyTypes = types;

            return propertyViewModel;

        }
        private PropertyDto GetPropertyDtoFromViewModel(PropertyEditViewModel propertyViewModel)
        {
            return new PropertyDto
            {
                Id=propertyViewModel.Id,
                Name = propertyViewModel.Name,
                Address = propertyViewModel.Address,
                AdministratorId = Convert.ToInt32(Request.Form["AdministratorId"]),
                CityId = Convert.ToInt32(Request.Form["CityId"]),
                Description = propertyViewModel.Description,
                NumberOfDaysForRefunds = propertyViewModel.NumberOfDaysForRefunds,
                Phone = propertyViewModel.Phone,
                PropetyTypeId = Convert.ToInt32(Request.Form["PropetyTypeId"]),
                Rating = propertyViewModel.Rating,
                TotalRooms = propertyViewModel.TotalRooms
            };

        }

        private  IEnumerable<SelectListItem> GetCities(int? selectedCityId)
        {

            return cityService.GetCities().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected=x.Id==selectedCityId }).ToList();
            
        }

        private IEnumerable<SelectListItem> GetAdmins(int? selectedAdminId)
        {
            return userService.GetAdministrators().Select(x=>new SelectListItem { Value = x.Id.ToString(), Text = x.FirstName+' '+x.LastName, Selected=x.Id==selectedAdminId}).ToList();
        }

        private IEnumerable<SelectListItem> GetPropertyTypes(int? selectedPropertyId)
        {
            return propertyTypeService.GetPropertyTypes().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Type, Selected = x.Id == selectedPropertyId }).ToList();
        }

        private async Task CreatePropertyImages(PropertyCreateViewModel propertyViewModel)
        {
            if (propertyViewModel.ListOfFiles != null)
            {
                foreach (var item in propertyViewModel.ListOfFiles)
                {
                    var url = await blobService.Upload(item);

                    var imageDto = new PropertyImageDto()
                    {
                        ImageUrl = url,
                        PropetyId = propertyService.GetLastId()

                    };
                    propertyImageSerive.CreatePropertyImage(imageDto);
                }
            }
        }
        
        private async Task DeleteImagesFromBlob(int id)
        {
            var propertyImages = propertyImageSerive.GetPropertyImageUrlByPropertyId(id);
            foreach (var item in propertyImages)
            {
                await blobService.Delete(item);
            }
        }

        #endregion
    }
}

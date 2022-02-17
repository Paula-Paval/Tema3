using Cinerva.Web.Models.Attribute;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cinerva.Web.Models
{
    public class PropertyEditViewModel: PropertyViewModel
    {
        public int CityId { get; set; }
        public int AdministratorId { get; set; }
        public int PropetyTypeId { get; set; }

        [DisplayName("City")]
        public IEnumerable<SelectListItem> ListOfCities { get; set; }
        [DisplayName("Admin")]
        public IEnumerable<SelectListItem> ListOfAdministrators{ get; set; }
        [DisplayName("Type")]
        public IEnumerable<SelectListItem> ListOfPropertyTypes { get; set; }
    }
}

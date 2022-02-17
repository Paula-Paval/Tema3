using Cinerva.Web.Models.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Cinerva.Web.Models
{
    public class PropertyCreateViewModel: PropertyEditViewModel
    {

        [DisplayName("Uploade image")]
        public IEnumerable<IFormFile>ListOfFiles { get; set; }
    }
}

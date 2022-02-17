using Cinerva.Web.Models.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinerva.Web.Models
{
    public class PropertyViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new valid name")]
        [LengthAttribute(ErrorMessage = "A valid name has more than 3 letters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new valid rating")]
        public decimal Rating { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new valid description")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new valid adress")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new valid phone")]
        public string Phone { get; set; }

        [DisplayName("Rooms")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new valid  number")]
        public int TotalRooms { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new valid number")]
        public int NumberOfDaysForRefunds { get; set; }
        [DisplayName("City")]
        public string CityName { get; set; }
        public string AdministratorName { get; set; }

        [DisplayName("Type")]
        public string PropetyTypeName { get; set; }

        [DisplayName("Images")]
        public IList<string> ListOfImages { get; set; }

    }
}

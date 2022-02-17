using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Users.Dto
{
    public  class UserDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsBanned { get; set; }
        public int RoleId { get; set; }
    }
}

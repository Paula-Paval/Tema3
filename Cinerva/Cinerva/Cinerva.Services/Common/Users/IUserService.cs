using Cinerva.Services.Common.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Users
{
    public interface IUserService
    {
        public IList<UserDto> GetAdministrators();
    }
}

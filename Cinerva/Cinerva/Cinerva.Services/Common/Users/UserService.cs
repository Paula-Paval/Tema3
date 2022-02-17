using Cinerva.Data;
using Cinerva.Services.Common.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Users
{
    public  class UserService:IUserService
    {
        private readonly CinervaDbContext dbContext;
        public UserService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<UserDto> GetAdministrators()
        {
            return dbContext.Users.Where(x=>x.Role.Name== "Admin").Select(x => new UserDto
            {
                Id = x.Id,
               FirstName=x.FirstName,
               LastName=x.LastName,
               Email=x.Email,
               Phone=x.Phone,
               IsDeleted=x.IsDeleted,
               IsBanned=x.IsBanned,
               RoleId =x.RoleId
             }).ToList();
        }

    }
}

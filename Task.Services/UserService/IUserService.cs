using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;

namespace Task.Services.UserService
{
    public interface IUserService
    {
        Task<int> UpdateUser(ApplicationUser user);
        Task<IList<ApplicationUser>> GetUsersInRole(string Role);
    }
}

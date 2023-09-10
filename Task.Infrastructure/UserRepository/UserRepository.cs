using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;
using Task.Core.Interfaces;

namespace Task.Infrastructure.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(IUnityOfWork unityOfWork , UserManager<ApplicationUser> userManager) 
        {
            _unityOfWork = unityOfWork;
            _userManager = userManager;
        }

        public async Task<int> UpdateUser(ApplicationUser user)
        {
            _unityOfWork.GetRepository<ApplicationUser>().UpdateOne(user);
            return await _unityOfWork.CompleteAsync(); 
        }

        public async Task<IList<ApplicationUser>> GetUsersInRole(string Role)
        {
            return await _userManager.GetUsersInRoleAsync(Role);
        }


    }
}

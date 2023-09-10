using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;
using Task.Infrastructure.UserRepository;

namespace Task.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<IList<ApplicationUser>> GetUsersInRole(string Role)
        {
            return await _userRepository.GetUsersInRole(Role);
        }

        public async Task<int> UpdateUser(ApplicationUser user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}

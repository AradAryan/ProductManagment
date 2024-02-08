using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Implementations
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper Mapper;
        private readonly UserManager<User> UserManager;

        public UserAppService(IMapper mapper, UserManager<User> userManager)
        {
            Mapper = mapper;
            UserManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(string username, string email, string password)
        {
            try
            {
                var user = new User { UserName = username, Email = email };
                var result = await UserManager.CreateAsync(user, password);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            return Mapper.Map<UserDto>(user);
        }

    }
}

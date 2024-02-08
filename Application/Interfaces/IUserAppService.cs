using Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IUserAppService
    {
        Task<IdentityResult> CreateUserAsync(string username, string email, string password);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<UserDto> GetUserByUsernameAsync(string username);
    }
}

using Mango.Services.AuthApi.Data;
using Mango.Services.AuthApi.Models;
using Mango.Services.AuthApi.Models.Dto;
using Mango.Services.AuthApi.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthApi.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser applicationUser = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            PhoneNumber = registrationRequestDto.PhoneNumber,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name
        };
        try
        {
            var result = await _userManager.CreateAsync(applicationUser,registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _appDbContext.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                UserDto user = new()
                {
                 Name = userToReturn.Name,
                 Email = userToReturn.Email,
                 Id = userToReturn.Id,
                 PhoneNumber = userToReturn.PhoneNumber
                };

                return "";
            }
            else
                return result.Errors.FirstOrDefault().Description;
        }
        catch (Exception ex)
        {

            return ex.Message;
        }
    }
}

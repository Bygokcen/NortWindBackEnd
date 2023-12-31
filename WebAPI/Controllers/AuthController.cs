using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController:Controller
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult Login(UserForLoginDto userForLoginDto)
    {
        var userToLogin = _authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }

        var result=_authService.CreateAccessToken(userToLogin.Data);
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.Message + "failed");
    }

    [HttpPost("register")]
    public ActionResult Register(UserForRegisterDto userForRegisterDto)
    {
        var userExist = _authService.UserExists(userForRegisterDto.Email);
        if (!userExist.Success)
        {
            return BadRequest(userExist.Message+"Burda bir sorun var");
        }

        var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
        var result = _authService.CreateAccessToken(registerResult.Data);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message + "failed son adım");

    }
}
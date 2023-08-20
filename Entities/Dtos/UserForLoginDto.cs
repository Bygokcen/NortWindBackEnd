using Core.Entities;

namespace Entities.Dtos;

public class UserForLoginDto:IDto
{
    public required string Email { get; set; } 
    public required string Password { get; set; } 
}
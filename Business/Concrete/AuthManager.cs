using Business.Abstract;
using Business.Constant;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete;

public class AuthManager:IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;



    public AuthManager(IUserService userService,ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }
    
    //REGISTER OPERATION
    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
    {
        HashingHelper.CreatePasswordHash(password,out var passwordHash,out var passwordSalt);
        var user = new User
        {
         Email   = userForRegisterDto.Email,
         FirstName = userForRegisterDto.FirstName,
         LastName = userForRegisterDto.LastName,
         PasswordHash = passwordHash,
         PasswordSalt = passwordSalt,
         Status = true
        };
        _userService.Add(user);

        return new SuccessDataResult<User>(user, Messages.UserRegistered);
    }

    
    
    // LOGIN OPERATION
    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByMail(userForLoginDto.Email);

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfullLogin);
    }

    public IResult UserExists(string email)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (_userService.GetByMail(email) != null)
        {
            return new ErrorResult(Messages.UserAlreadyExists);
        }
        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims =_userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }
}
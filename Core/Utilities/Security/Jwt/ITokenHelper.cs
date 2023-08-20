using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt;

public interface ITokenHelper
{
    //kulllanıcı bilgisi ve operasyon
    AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    
    
}
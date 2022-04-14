using Microsoft.AspNetCore.Identity;
using Poll.Interfaces;

namespace Poll.App
{
    public class CheckPwd
    {
        public static bool CheckValidPwd(Models.User user, IUserService userService)
        {
            var hashPwd = userService.GetAsync(user.Id).Result;
            var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, hashPwd.Password, user.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
                return true;

            return false;
        }
    }
}

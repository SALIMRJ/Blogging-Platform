using Blogging_Platform.Domain.Model.Auth;

namespace Blogging_Platform.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(AuthRequestModel model);
        Task<AuthModel> LoginAsync(AuthLoginModle model);
    }
}

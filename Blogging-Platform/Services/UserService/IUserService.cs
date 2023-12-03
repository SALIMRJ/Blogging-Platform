using Blogging_Platform.Domain.Model.Post;
using Blogging_Platform.Domain.Model.User;

namespace Blogging_Platform.Services.UserService
{
    public interface IUserService
    {
        Task<UserModel> GetByIdAsync(string id);
    }
}

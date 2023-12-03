using Blogging_Platform.Domain.Model.Post;

namespace Blogging_Platform.Services.PostService
{
    public interface IPostService
    {
        Task<PostModel> GetByIdAsync(long id, string userId);
        Task<PostModel> CreateAsync(PostRequestModel model, string userId);
        Task<PostModel> UpdateAsync(long id, PostRequestModel model, string userId);
        Task<List<PostModel>> GetAllAsync(string userId);
        Task DeleteAsync(long id);
    }
}

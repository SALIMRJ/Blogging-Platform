using AutoMapper;
using Blogging_Platform.DB.Repositories;
using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Model.Post;
using Blogging_Platform.Domain.Repo;

namespace Blogging_Platform.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;
        private readonly IMapper _Mapper;
        public PostService(IRepository<Post> repository, IMapper Mapper)
        {
            _repository = repository;
            _Mapper = Mapper;
        }
        public async Task<PostModel> CreateAsync(PostRequestModel model, string userId)
        {
            try
            {
                var mappedEntity = _Mapper.Map<Post>(model);
                mappedEntity.DateCreated = DateTime.UtcNow;
                mappedEntity.ApplicationUserId = userId;
                var result = await _repository.AddAsync(mappedEntity);
                return _Mapper.Map<PostModel>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                _repository.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PostModel>> GetAllAsync(string userId)
        {
            try
            {
                var postList = await _repository.GetAllAsync(x => x.ApplicationUserId == userId);
                return _Mapper.Map<List<PostModel>>(postList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PostModel> GetByIdAsync(long id, string userId)
        {
            try
            {
                var post = await _repository.GetByIdAsync(id, x => x.ApplicationUserId == userId);
                return _Mapper.Map<PostModel>(post);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PostModel> UpdateAsync(long id, PostRequestModel model, string userId)
        {
            try
            {
                var mappedEntity = _Mapper.Map<Post>(model);
                mappedEntity.DateModified = DateTime.UtcNow;
                mappedEntity.ApplicationUserId = userId;
                mappedEntity.Id= id;
                var result = _repository.UpdateAsync(mappedEntity);
                return _Mapper.Map<PostModel>(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

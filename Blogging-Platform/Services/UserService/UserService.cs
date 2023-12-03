using AutoMapper;
using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Model.Post;
using Blogging_Platform.Domain.Model.User;
using Blogging_Platform.Domain.Repo;

namespace Blogging_Platform.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IMapper _Mapper;
        public UserService(IRepository<ApplicationUser> repository, IMapper Mapper)
        {
            _repository = repository;
            _Mapper = Mapper;
        }
        public async Task<UserModel> GetByIdAsync(string id)
        {
            try
            {
                var result = await _repository.Find(x => x.Id == id, new[] { "Posts" });
                var mappedResult= _Mapper.Map<UserModel>(result);

                return mappedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

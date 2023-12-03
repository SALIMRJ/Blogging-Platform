using AutoMapper;
using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Model.Post;
using Blogging_Platform.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.AutoMapper
{

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserModel>();
     

            CreateMap<PostRequestModel, Post>();
        }
    }
}

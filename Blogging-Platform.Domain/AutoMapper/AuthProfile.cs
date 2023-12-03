using AutoMapper;
using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Model.Auth;
using Blogging_Platform.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.AutoMapper
{

    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthRequestModel, AuthModel>();

            CreateMap<AuthRequestModel, ApplicationUser>();
            CreateMap<ApplicationUser, AuthModel>();

        }
    }
}

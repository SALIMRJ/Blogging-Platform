using AutoMapper;
using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostModel>()
                .ForMember(dist => dist.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dist => dist.AuthorId, opt => opt.MapFrom(src => src.ApplicationUserId)).ReverseMap();

            CreateMap<PostRequestModel, Post>();
        }
    }
}

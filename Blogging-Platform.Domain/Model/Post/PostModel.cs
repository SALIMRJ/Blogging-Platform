using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.Model.Post
{
    public class PostModel : PostRequestModel
    {
        public long PostId { get; set; }
        public string AuthorId  { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

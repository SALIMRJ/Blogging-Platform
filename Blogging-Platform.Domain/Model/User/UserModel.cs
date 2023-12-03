using Blogging_Platform.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.Model.User
{
    public class UserModel 
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<PostModel> Posts{ get; set; }

    }
}

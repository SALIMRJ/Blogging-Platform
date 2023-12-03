using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.Entity
{
    public class Post
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public string Content { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

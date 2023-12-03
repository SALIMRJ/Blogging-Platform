using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.Model.Auth
{
    public class AuthLoginModle
    {
        [Required,StringLength(100)]
        public string UserName { get; set; }
        [Required, StringLength(100)]
        public string  Password { get; set; }
    }
}

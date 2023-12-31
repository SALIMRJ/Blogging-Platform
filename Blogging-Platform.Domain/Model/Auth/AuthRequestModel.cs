﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.Model.Auth
{
    public class AuthRequestModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required, StringLength(100)]
        public string UserName { get; set; }
        [Required, StringLength(100),EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
    }
}

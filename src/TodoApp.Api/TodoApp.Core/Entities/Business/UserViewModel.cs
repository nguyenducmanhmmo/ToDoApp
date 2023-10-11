﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Entities.Business
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [Required, StringLength(maximumLength: 150)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 150)]
        public string Password { get; set; }
    }
}

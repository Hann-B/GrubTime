﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
 
namespace GrubTime.Models
{
    public class UserProfile
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }
}

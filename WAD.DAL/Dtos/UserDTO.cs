﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD.DAL.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public List<ActivityDTO> Activities { get; set; } // Associated activities
    }
}
﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Dto
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}

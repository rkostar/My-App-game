﻿using System.ComponentModel.DataAnnotations;

namespace MyApp.Model
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}

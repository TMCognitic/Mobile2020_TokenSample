using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile2020_TokenSample.Models.Forms
{
    public class LoginForm
    {
        [Required]
        [MaxLength(320)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Passwd { get; set; }
    }
}

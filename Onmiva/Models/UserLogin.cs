using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Onmiva.Models
{
    public class UserLogin
    {
        [Display(Name = "El paštas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El paštas privalomas")]
        public string EmailID { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Slaptažodis privalomas")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Prisiminti mane")]
        public bool RememberMe { get; set; }
    }
}
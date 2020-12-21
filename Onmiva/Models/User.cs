using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{ 

    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public int UserId { get; set; }

        [Display(Name = "Vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vardas privalomas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavardė privaloma")]
        public string LastName { get; set; }

        [Display(Name = "El. Paštas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El. paštas privalomas")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Slaptažodis privalomas")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Reikia bent 6-ių simbolių")]
        public string Password { get; set; }

        [Display(Name = "Slaptažodžio patvirtinimas")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa")]
        public string ConfirmPassword { get; set; }

        public bool IsMailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }
        public string Role { get; set; }



    }

    public class UserMetadata
    {
        [Display(Name = "Vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vardas privalomas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavardė privaloma")]
        public string LastName { get; set; }

        [Display(Name = "El. Paštas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El. paštas privalomas")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Slaptažodis privalomas")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Reikia bent 6-ių simbolių")]
        public string Password { get; set; }

        [Display(Name = "Slaptažodžio patvirtinimas")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa")]
        public string ConfirmPassword { get; set; }

    }
}
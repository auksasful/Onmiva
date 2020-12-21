using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    [MetadataType(typeof(VacationCalendarMetaData))]
    public partial class VacationCalendar
    {
        public int AtostogosId { get; set; }


        [Display(Name = "Vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vardas būtinas")]
        public string vardas { get; set; }

        [Display(Name = "Pavarde")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavarde būtina")]
        public string pavarde { get; set; }


        [Display(Name = "Pradzios data")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pradzios data būtina")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public Nullable<System.DateTime> pradzios_data { get; set; }

        [Display(Name = "Pabaigos data")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pabaigos data būtina")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public Nullable<System.DateTime> pabaigos_data { get; set; }



    }

    public class VacationCalendarMetaData
    {


        [Display(Name = "Vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vardas būtinas")]
        public string vardas { get; set; }

        [Display(Name = "Pavarde")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavarde būtina")]
        public string pavarde { get; set; }

        [Display(Name = "Pradzios data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public string pradzios_data { get; set; }

        [Display(Name = "Pabaigos data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public string pabaigos_data { get; set; }





    }
}
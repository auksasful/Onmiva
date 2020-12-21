using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    [MetadataType(typeof(ClientReservationMetaData))]
    public partial class ClientReservation
    {
        public int rezervacijosId { get; set; }


        [Display(Name = "Pavadinimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavadinimas būtinas")]
        public string pavadinimas { get; set; }

        [Display(Name = "Vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vardas būtinas")]
        public string klientoVardas { get; set; }


        [Display(Name = "Pavarde")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavarde būtina")]
        public string klientoPavarde { get; set; }

        [Display(Name = "Rezervacijos data")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Rezervacijos data būtina")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public Nullable<System.DateTime> rezervacijos_data { get; set; }



    }

    public class ClientReservationMetaData
    {


        [Display(Name = "Pavadinimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavadinimas būtinas")]
        public string pavadinimas { get; set; }

        [Display(Name = "Vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vardas būtinas")]
        public string klientoVardas { get; set; }

        [Display(Name = "Pavarde")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavarde būtina")]
        public string klientoPavarde { get; set; }

        [Display(Name = "Rezervacijos data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public string rezervacijos_data { get; set; }





    }
}
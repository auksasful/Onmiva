using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    public class Payment
    {
        [Display(Name = "Numeris")]
        public int Id { get; set; }

        [Display(Name = "Suma")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Suma negali būti tuščia")]
        public decimal Sum { get; set; }


        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY-MM-DD}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data negali būti tuščia")]
        public DateTime Date { get; set; }

        [Display(Name = "Apmokėjimo metodas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Apmokėjimo metodas negali būti tuščias")]
        public string PaymentMethod { get; set; }

        public int SenderCompany { get; set; }
        public int Bill { get; set; }
        public int Client { get; set; }


    }
}
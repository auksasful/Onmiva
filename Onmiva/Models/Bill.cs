using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    public class Bill
    {

        [Display(Name = "Numeris")]
        public int NumberId { get; set; }

        [Display(Name = "Suma")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Suma negali būti tuščia")]
        public decimal Sum { get; set; }


        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY-MM-DD}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data negali būti tuščia")]
        public DateTime Date { get; set; }

        [Display(Name = "Mokėjimo paskirtis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mokėjimo paskirtis negali būti tuščia")]
        public string PayComment { get; set; }


        [Display(Name = "Apmokėti iki")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Laukas Apmokėti iki negali būti tuščias")]
        public DateTime PayUntil { get; set; }

        [Display(Name = "Būsena")]
        public string State { get; set; }
        public int StateId { get; set; }

        [Display(Name = "Siuntimo įmonė")]
        public string SendingCompany { get; set; }
        public int SendingCompanyId { get; set; }

        [Display(Name = "Užsakymas")]
        public int OrderId { get; set; }

        [Display(Name = "Užsakymas")]
        public string Order { get; set; }

        

    }
}
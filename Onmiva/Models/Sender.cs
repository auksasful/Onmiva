using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{ 

    [MetadataType(typeof(SenderMetaData))]
    public partial class Sender
    {
        [Display(Name = "id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "id būtinas")]
        public int SenderId { get; set; }


        [Display(Name = "Pavadinimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavadinimas būtinas")]
        public string pavadinimas { get; set; }

        [Display(Name = "Tel. nr.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tel.nr. būtinas")]
        public string tel_nr { get; set; }


        [Display(Name = "Miestas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Miestas būtinas")]
  
        public string miestas { get; set; }

   
       
    }

    public class SenderMetaData
    {
        [Display(Name = "id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "id būtinas")]
        public int SenderId { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavadinimas būtinas")]
        public string pavadinimas { get; set; }

        [Display(Name = "Tel. nr.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tel.nr. būtinas")]
        public string tel_nr { get; set; }


        [Display(Name = "Miestas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Miestas būtinas")]

        public string miestas { get; set; }


    }
}
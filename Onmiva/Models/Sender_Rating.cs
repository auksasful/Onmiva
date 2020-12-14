using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{ 

    [MetadataType(typeof(SenderRatingMetaData))]
    public partial class Sender_Rating
    {
        public int SenderId { get; set; }


        [Display(Name = "Įvertinimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Įvertinimas būtinas")]
        public int ivertinimas { get; set; }


        [Display(Name = "Atsiliepimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavadinimas būtinas")]
        public string atsiliepimas { get; set; }



    }

    public class SenderRatingMetaData
    {
        public int SenderId { get; set; }

        [Display(Name = "Įvertinimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Įvertinimas būtinas")]
        public int ivertinimas { get; set; }

        [Display(Name = "Atsiliepimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Atsiliepimas būtinas")]
        public string atsiliepimas { get; set; }

    }
}
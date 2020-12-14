using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{ 

    [MetadataType(typeof(SenderReviewMetaData))]
    public partial class Sender_Review
    {
        public int SenderId { get; set; }


        [Display(Name = "Atsiliepimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Atsiliepimas būtinas")]
        public string atsiliepimas { get; set; }



   
       
    }

    public class SenderReviewMetaData
    {


        public int SenderId { get; set; }


        [Display(Name = "Atsiliepimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Atsiliepimas būtinas")]
        public string atsiliepimas { get; set; }

        public int siuntejas { get; set; }
    }
}
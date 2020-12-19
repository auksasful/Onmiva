using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{ 

    [MetadataType(typeof(ContractMetaData))]
    public partial class Contract
    {
        public int ContractId { get; set; }


        [Display(Name = "Įmonė")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Įmonė būtina")]
        public int imone { get; set; }

        [Display(Name = "Siuntėjas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Siuntėjas būtinas")]
        public int siuntejas { get; set; }


        [Display(Name = "Pasirašymo data")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pasirašymo data būtina")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> pasirasymo_data { get; set; }

        [Display(Name = "Pabaigos data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> pabaigos_data { get; set; }

       
    }

    public class ContractMetaData
    {


        [Display(Name = "Įmonė")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Įmonė būtina")]
        public int imone { get; set; }

        [Display(Name = "Siuntėjas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Siuntėjas būtinas")]
        public int siuntejas { get; set; }

        [Display(Name = "Pasirašymo data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string pasirasymo_data { get; set; }

        [Display(Name = "Pabaigos data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string pabaigos_data { get; set; }

      

    }
}
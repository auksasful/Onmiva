using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    [MetadataType(typeof(TeamBuildingOffersMetaData))]
    public partial class TeamBuildingOffers
    {
        public int TeamBuildingId { get; set; }


        [Display(Name = "Data")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "data būtina")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public Nullable<System.DateTime> data { get; set; }

        [Display(Name = "Pasiulymas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pasiulymas būtinas")]
        public string pasiulymas { get; set; }


        [Display(Name = "Islaidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Islaidos būtinos")]
        public int islaidos { get; set; }



    }

    public class TeamBuildingOffersMetaData
    {

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY/MM/DD}")]
        public string data { get; set; }

        [Display(Name = "Pasiulymas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pasiulymas būtinas")]
        public string pasiulymas { get; set; }

        [Display(Name = "Islaidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Islaidos būtinos")]
        public int islaidos { get; set; }





    }
}
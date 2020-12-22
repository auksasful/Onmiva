using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    [MetadataType(typeof(ShipmentCompanyMetadata))]
    public partial class ShipmentCompany
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id būtinas")]
        public int Id { get; set; }
    }

    public class ShipmentCompanyMetadata
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id būtinas")]
        public int Id { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pavadinimas būtinas")]
        public string pavadinimas { get; set; }
    }
}
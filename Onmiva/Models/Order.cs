using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    [MetadataType(typeof(OrderMetaData))]
    public partial class Order
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id būtinas")]
        public int Id { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY-MM-DD}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data negali būti tuščia")]
        public DateTime Date { get; set; }

        [Display(Name = "Suma")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Suma negali būti tuščia")]
        public decimal Amount { get; set; }

        [Display(Name = "Užsakymo būsena")]
        public string OrderStatus { get; set; }

        [Display(Name = "Siuntų įmonė")]
        public string ShipmentCompany { get; set; }

        [Display(Name = "Klientas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Klientas būtinas")]
        public string Client { get; set; }
    }

    public class OrderMetaData
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id būtinas")]
        public int Id { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:YYYY-MM-DD}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data negali būti tuščia")]
        public DateTime Date { get; set; }

        [Display(Name = "Suma")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Suma negali būti tuščia")]
        public decimal Amount { get; set; }

        [Display(Name = "Užsakymo būsena")]
        public string OrderStatus { get; set; }

        [Display(Name = "Siuntų įmonė")]
        public string ShipmentCompany { get; set; }

        [Display(Name = "Klientas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Klientas būtinas")]
        public string Client { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Onmiva.Models
{
    public class Order_Bill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public double Weight { get; set; }
        public string State { get; set; }
        public string StateId { get; set; }
        public string Company { get; set; }
        public int CompanyId { get; set; }
        public string Client { get; set; }
        public int ClientId { get; set; }

    }
}
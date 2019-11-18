using System;
using System.Collections.Generic;

namespace Art.Models
{
    public partial class Invoice
    {
        public int IdInvoice { get; set; }
        public Guid FkGuidUser { get; set; }
        public int FkIdEvent { get; set; }
        public decimal Cost { get; set; }
        public DateTime InvoiceDate { get; set; }

        public Users FkGuidUserNavigation { get; set; }
        public Events FkIdEventNavigation { get; set; }
    }
}

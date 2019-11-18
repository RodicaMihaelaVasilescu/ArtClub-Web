using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Art.Models
{
    public partial class Events
    {
        public Events()
        {
            Invoice = new HashSet<Invoice>();
        }
        public int IdEvent { get; set; }
        public string EventName { get; set; }
        public string Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Tag { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}

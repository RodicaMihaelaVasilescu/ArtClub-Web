using System;
using System.Collections.Generic;

namespace Art.Models
{
    public partial class EventUsers
    {
        public Guid GuidEventUser { get; set; }
        public int FkIdEvent { get; set; }
        public Guid FkGuidUser { get; set; }
    }
}

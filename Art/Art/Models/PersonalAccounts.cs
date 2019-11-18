using System;
using System.Collections.Generic;

namespace Art.Models
{
    public partial class PersonalAccounts
    {
        public int IdPersonalAccount { get; set; }
        public Guid FkGuidUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }

        public Users FkGuidUserNavigation { get; set; }
    }
}

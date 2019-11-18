using System;
using System.Collections.Generic;

namespace Art.Models
{
    public partial class Users
    {
        public Users()
        {
            Invoice = new HashSet<Invoice>();
            PersonalAccounts = new HashSet<PersonalAccounts>();
        }

        public Guid GuidUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<PersonalAccounts> PersonalAccounts { get; set; }
    }
}

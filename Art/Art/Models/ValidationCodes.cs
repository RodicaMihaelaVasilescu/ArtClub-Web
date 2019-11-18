using System;
using System.Collections.Generic;

namespace Art.Models
{
    public partial class ValidationCodes
    {
        public int IdConfirmationCode { get; set; }
        public string Username { get; set; }
        public int Code { get; set; }
        public int AttemptsNumber { get; set; }
    }
}

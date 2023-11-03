using System;
using System.Collections.Generic;

namespace VineforceJitendra.Models
{
    public partial class StateList
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual CountryList Country { get; set; } = null!;
    }
}

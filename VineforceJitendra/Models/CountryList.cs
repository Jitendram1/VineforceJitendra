using System;
using System.Collections.Generic;

namespace VineforceJitendra.Models
{
    public partial class CountryList
    {
        public CountryList()
        {
            StateLists = new HashSet<StateList>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;

        public virtual ICollection<StateList> StateLists { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectSazan.Domain
{
    public class Quote
    {
        public Guid Id { get; set; }
        public UserIdentity Collector { get; set; }
        public IEnumerable<Guid> ItemsToInsure { get; set; }
        public IEnumerable<Insurer> Insurers { get; set; }
    }
}

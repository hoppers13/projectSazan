using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectSazan.Domain
{
    public class PhilatelicCollection
    {
        public Guid Id { get; set; }
        public string CollectorId { get; set; }
        public string Title { get; set; }
        public IEnumerable<PhilatelicItem> Items { get; set; } 
    }
}

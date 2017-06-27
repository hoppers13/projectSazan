using System;
using System.Collections.Generic;

namespace ProjectSazan.Domain.Philately
{

    public class PhilatelicCollection : IPhilatelicCollection
    {
        public Guid Id { get; set; }
        public string CollectorId { get; set; }
        public string Title { get; set; }
        public IList<PhilatelicItem> Items { get; set; }     
    }
}

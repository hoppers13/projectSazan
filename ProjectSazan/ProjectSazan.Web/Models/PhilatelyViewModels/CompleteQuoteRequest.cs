using ProjectSazan.Domain;
using ProjectSazan.Domain.Philately;
using System.Collections.Generic;

namespace ProjectSazan.Web.Models.PhilatelyViewModels
{
    public class CompleteQuoteRequest
    {
        public UserIdentity Collector { get; set; }
        public IEnumerable<PhilatelicItem> Items { get; set; }
        public IEnumerable<Insurer> AllAvailableImsurers { get; set; }
    }
}

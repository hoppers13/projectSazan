using ProjectSazan.Domain;
using System.Collections.Generic;

namespace ProjectSazan.Web.Persistence.FileSystem
{
	internal class CollectionsSummary
    {
		public UserIdentity Collector { get; set; } 
		public IList<ColletionSummary> Collections { get; set; }
    }
}

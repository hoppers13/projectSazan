using ProjectSazan.Domain.Philately;
using System;
using System.Collections.Generic;

namespace ProjectSazan.Persistence.InMemory.SampleData
{
	internal static class SampleData
	{
		internal static IEnumerable<PhilatelicCollection> PhilatelicCollections
		{
			get
			{
				return 
					new List<PhilatelicCollection>
					{
						  new PhilatelicCollection
						  {
							   Id = Guid.NewGuid(),
							   CollectorId = "gianluca@email.com",
							   Title = "Italian Islands ot the Mediterranean",
							   Items = new List<PhilatelicItem>
							   {
								   new PhilatelicItem{
									   Id = Guid.NewGuid(),
									   Year = 1929,
									   CatalogueReference = new CatalogueReference{ Catalogue = CataloguesInUse.SASSONE, Area = "Egeo", Number = "3/11 S.2"},
									   Description = "Pittorica",
									   Conditions = Conditions.MNH
								   },
								   new PhilatelicItem{
									   Id = Guid.NewGuid(),
									   Year = 1933,
									   CatalogueReference = new CatalogueReference{ Catalogue = CataloguesInUse.SASSONE, Area = "Egeo", Number = "A 22/27 S.31"},
									   Description = "Crociera Zeppelin",
									   Conditions = Conditions.MNH
								   },
								   new PhilatelicItem{
									   Id = Guid.NewGuid(),
									   Year = 1934,
									   CatalogueReference = new CatalogueReference{ Catalogue = CataloguesInUse.SASSONE, Area = "Egeo", Number = "n/a"},
									   Description = "Ferrucci, saggi per tutte le isole e posta aerea",
									   Conditions = Conditions.MNH
								   }
							   }
						  },
						  // empty collection
						  new PhilatelicCollection
						  {
							   Id = Guid.NewGuid(),
							   CollectorId = "gianluca@email.com",
							   Title = "Dalmatia"
						  }
					};
			}
		}		
	}
}
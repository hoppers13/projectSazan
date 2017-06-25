using ProjectSazan.Domain;
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
									   Conditions = Conditions.MNH,
                                       Scans = new Scans
                                       {
                                          new Scan{ Image = @"\images\scans\Egeo.1929.Pittorica.Sassone3-11.jpg", Caption = "1929 Pittorica, fronte" },
                                          new Scan{ Image = @"\images\scans\Egeo.1929.Pittorica.Sassone3-11.retro.jpg", Caption = "1929 Pittorica, retro" },
                                          new Scan{ Image = @"\images\scans\Egeo.1929.Pittorica.Sassone3-11.certificate.jpg", Caption = "1929 Pittorica, certificate" },
                                       },                                       
									   Acquired = new DateTime(2017, 4, 11),
									   Paid = new Price{Currency = Currency.EUR, Figure = 180}
								   },
								   new PhilatelicItem{
									   Id = Guid.NewGuid(),
									   Year = 1933,
									   CatalogueReference = new CatalogueReference{ Catalogue = CataloguesInUse.SASSONE, Area = "Egeo", Number = "A 22/27 S.31"},
									   Description = "Crociera Zeppelin",
									   Conditions = Conditions.MNH,
                                       Scans = new Scans
                                       {
                                          new Scan{Image = @"\images\scans\Egeo.1930.CrocieraZeppelin.Sassone22-27.jpg", Caption = "1933 Crociera Zeppelin, fronte" },                                          
                                       },
                                       Acquired = new DateTime(2017, 5, 18),
									   Paid = new Price{Currency = Currency.EUR, Figure = 260}
								   },
								   new PhilatelicItem{
									   Id = Guid.NewGuid(),
									   Year = 1934,
									   CatalogueReference = new CatalogueReference{ Catalogue = CataloguesInUse.SASSONE, Area = "Egeo", Number = "n/a"},
									   Description = "Ferrucci, saggi per tutte le isole e posta aerea",
									   Conditions = Conditions.MNH,
                                       Scans = new Scans(){ Scan.NoScan},
                                       Acquired = new DateTime(2017, 4, 1),
									   Paid = new Price{Currency = Currency.EUR, Figure = 240}
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
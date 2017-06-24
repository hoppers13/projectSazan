using ProjectSazan.Domain.Philately;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSazan.Persistence.InMemory
{
    internal static class InMemoryStore
    {
		public static Dictionary<Guid, IPhilatelicCollection> PhilatelicCollections { get; private set; }
		public static Dictionary<string, IList<Guid>> CollectorColletions { get; private set; }		


		static InMemoryStore()
		{
			PhilatelicCollections = new Dictionary<Guid, IPhilatelicCollection>();
			CollectorColletions = new Dictionary<string, IList<Guid>>();

			LoadSampleData();
		}

		private static void LoadSampleData()
		{
			// load collections
			foreach(var collection in SampleData.SampleData.PhilatelicCollections)
			{
				PhilatelicCollections.Add(collection.Id, collection);
			}

			// associate collector to collections
			var collector = "gianluca@email.com";
			CollectorColletions.Add(collector, new List<Guid>
			{
				PhilatelicCollections.Values.FirstOrDefault(coll => coll.Title == "Italian Islands ot the Mediterranean").Id, 
				PhilatelicCollections.Values.FirstOrDefault(coll => coll.Title == "Dalmatia").Id, 
			});
		}
	}
}
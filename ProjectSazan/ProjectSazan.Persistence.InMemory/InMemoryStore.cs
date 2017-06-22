using ProjectSazan.Domain.Philately;
using System.Collections.Generic;

namespace ProjectSazan.Persistence.InMemory
{
    internal static class InMemoryStore
    {
		public static Dictionary<string, IEnumerable<PhilatelicCollection>> PhilatelicCollections { get; private set; }

		static InMemoryStore()
		{
			PhilatelicCollections = new Dictionary<string, IEnumerable<PhilatelicCollection>>();

			LoadSampleData();
		}

		private static void LoadSampleData()
		{
			PhilatelicCollections = SampleData.SampleData.PhilatelicCollections;
		}
	}
}
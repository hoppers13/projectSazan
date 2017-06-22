using ProjectSazan.Domain;
using System.Collections.Generic;

namespace ProjectSazan.Persistence.InMemory
{
	internal static class InMemoryStore
    {
		public static Dictionary<string, IEnumerable<string>> PhilatelicCollections { get; private set; }

		static InMemoryStore()
		{
			PhilatelicCollections = new Dictionary<string, IEnumerable<string>>();

			LoadSampleData();
		}

		private static void LoadSampleData()
		{
			PhilatelicCollections = SampleData.SampleData.Collections;
		}
	}
}

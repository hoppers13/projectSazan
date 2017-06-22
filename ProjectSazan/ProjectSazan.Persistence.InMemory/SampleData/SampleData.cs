using System.Collections.Generic;

namespace ProjectSazan.Persistence.InMemory.SampleData
{
	internal static class SampleData
    {
		internal static Dictionary<string, IEnumerable<string>> Collections
		{
			get
			{
				var ret = new Dictionary<string, IEnumerable<string>>
				{
					{
						"gianluca@email.com" ,
						new List<string>{
							"Italian Islandas ot the Mediterranean",
							"Dalmatia"
						}
					},
					{
						"nancy@email.com",
						new List<string>{
							"Dartmoor",
							"Cambridshire"
						}
					}
				};
				return ret;				
			}
		}
    }
}

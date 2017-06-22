using ProjectSazan.Domain.Philately;
using System;
using System.Collections.Generic;

namespace ProjectSazan.Persistence.InMemory.SampleData
{
	internal static class SampleData
    {
        internal static Dictionary<string, IEnumerable<PhilatelicCollection>> PhilatelicCollections
        {
            get
            {
                return new Dictionary<string, IEnumerable<PhilatelicCollection>>
                {
                    { "gianluca@email.com",
                      new List<PhilatelicCollection>
                      {
                          new PhilatelicCollection
                          {
                               Id = Guid.NewGuid(),
                               CollectorId = "gianluca@email.com",
                               Title = "Italian Islands ot the Mediterranean"
                          },
                          new PhilatelicCollection
                          {
                               Id = Guid.NewGuid(),
                               CollectorId = "gianluca@email.com",
                               Title = "Dalmatia"
                          }
                      }
                    }
                };
            }
        }
    }
}

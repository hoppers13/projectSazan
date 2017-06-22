using System.Collections.Generic;

namespace ProjectSazan.Domain.Philately
{
    public interface IPhilatelicCollection : ICollectableCollection
    {
        IEnumerable<PhilatelicItem> Items { get; set; }
    }
}

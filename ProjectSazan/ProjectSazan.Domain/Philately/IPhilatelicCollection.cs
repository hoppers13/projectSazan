using System.Collections.Generic;

namespace ProjectSazan.Domain.Philately
{
    public interface IPhilatelicCollection : ICollectableCollection
    {
        IList<PhilatelicItem> Items { get; set; }
    }
}

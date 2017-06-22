using System;

namespace ProjectSazan.Domain
{
    public interface ICollectableCollection
    {
        Guid Id { get; set; }
        string CollectorId { get; set; }
        string Title { get; set; }        
    }
}

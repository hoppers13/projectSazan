using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectSazan.Domain;

namespace ProjectSazan.Persistence
{
    public interface IQuoteRepository
    {
        Task AddIncompleteQuote(Quote quote);
        Task<IEnumerable<Insurer>> GetAllInsurersAsync();
    }
}

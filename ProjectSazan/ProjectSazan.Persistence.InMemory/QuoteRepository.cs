using System.Threading.Tasks;
using ProjectSazan.Domain;
using System.Collections.Generic;
using System;

namespace ProjectSazan.Persistence.InMemory
{
    public class QuoteRepository : IQuoteRepository
    {
        public Task<IEnumerable<Insurer>> GetAllInsurersAsync()
        {
            return Task.Run(() =>
            {
               return  InMemoryStore.Insurers.Values as IEnumerable<Insurer>;
            });

            throw new NotImplementedException();
        }

        Task IQuoteRepository.AddIncompleteQuote(Quote quote)
        {
            var collector = quote.Collector.Id;

            return Task.Run(() => {
                if (InMemoryStore.IncompleteQuotes.ContainsKey(collector))
                {
                    InMemoryStore.IncompleteQuotes[collector].Add(quote);
                }
                else
                {
                    InMemoryStore.IncompleteQuotes[collector] = new List<Quote> { quote };
                }
            });            
        }
    }
}

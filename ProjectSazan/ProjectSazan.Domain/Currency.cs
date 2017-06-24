using System.Collections.Generic;

namespace ProjectSazan.Domain
{
	public enum Currency
    {
		GBP,
		EUR,
		USD
    }

	public class CurrencySymbols : Dictionary<Currency, string>
	{
		public CurrencySymbols()
		{
			this[Currency.GBP] = "£";
			this[Currency.EUR] = "€";
			this[Currency.USD] = "$";			
		}
	}
}
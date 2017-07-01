using System;

namespace ProjectSazan.Domain.Philately
{
    public class PhilatelicItem : ICollectable
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public PhilatelicItemType Type { get; set; }
        public string Description { get; set; }
        public Conditions Conditions { get; set; }
        public CatalogueReference CatalogueReference { get; set; }
        public Scans Scans { get; set; }
                
        public DateTime Acquired { get; set; }
		public Price Paid { get; set; }

        public override string ToString()
        {
            return $"{Year}, {CatalogueReference.Area} {Description} ({new CatalogueAbbreviations()[CatalogueReference.Catalogue]} {CatalogueReference.Number})";
        }
        
        //TODO: unit tests
        public bool IsSameItem(CatalogueReference reference)
        {
            return
                CatalogueReference.Catalogue == reference.Catalogue
                && CatalogueReference.Area == reference.Area
                && CatalogueReference.Number == reference.Number;            
        }        
    }
}
namespace ProjectSazan.Domain.Philately
{
    public struct CatalogueReference
    {
        public CataloguesInUse Catalogue { get; set; }
        public string Area { get; set; }
        public string Number { get; set; }        
    }
}

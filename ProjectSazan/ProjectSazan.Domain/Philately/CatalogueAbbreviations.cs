using System.Collections.Generic;

namespace ProjectSazan.Domain.Philately
{
    public class CatalogueAbbreviations : Dictionary<CataloguesInUse, string>
    {
        public CatalogueAbbreviations()
        {
            this[CataloguesInUse.STANLEY_GIBBONS] = "SG";
            this[CataloguesInUse.MICHEL] = "Mi.";
            this[CataloguesInUse.YVERT_TELLIER] = "YT";
            this[CataloguesInUse.SCOTT] = "Scott";
            this[CataloguesInUse.SASSONE] = "Sass.";
        }
    }
}

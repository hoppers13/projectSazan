﻿using System;

namespace ProjectSazan.Domain.Philately
{
    public class PhilatelicItem
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public PhilatelicItemType Type { get; set; }
        public string Description { get; set; }
        public Conditions Conditions { get; set; }
        public CatalogueReference CatalogueReference { get; set; }
        
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
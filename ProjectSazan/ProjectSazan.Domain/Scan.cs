using System.Collections.Generic;

namespace ProjectSazan.Domain
{
    public class Scan
    {
        public string Image { get; set; }
        public string Caption { get; set; }

        public static Scan NoScan
        {
            get
            {
                return new Scan { Image = string.Empty, Caption = string.Empty };
            }
        } 
    }

    public class Scans : List<Scan>
    {
    }
}
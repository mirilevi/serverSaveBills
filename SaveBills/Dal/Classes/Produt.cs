using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes
{
    public partial class Produt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int? Category { get; set; }
        public double? Price { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Category CategoryNavigation { get; set; }
    }
}

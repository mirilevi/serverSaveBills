using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes
{
    public partial class BillCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Category Category { get; set; }
    }
}

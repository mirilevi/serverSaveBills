using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes

{
    public partial class ExpiredBill
    {
        public int Id { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; }
    }
}

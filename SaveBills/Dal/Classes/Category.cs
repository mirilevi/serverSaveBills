using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes
{
    public partial class Category
    {
        public Category()
        {
            BillCategories = new HashSet<BillCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<BillCategory> BillCategories { get; set; }
    }
}

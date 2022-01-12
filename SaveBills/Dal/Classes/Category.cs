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
            UserCategories = new HashSet<UserCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<BillCategory> BillCategories { get; set; }
        public virtual ICollection<UserCategory> UserCategories { get; set; }
    }
}

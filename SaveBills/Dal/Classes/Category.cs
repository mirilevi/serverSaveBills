using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes
{
    public partial class Category
    {
        public Category()
        {
            Produts = new HashSet<Produt>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Produt> Produts { get; set; }
    }
}

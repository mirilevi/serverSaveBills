using System;
using System.Collections.Generic;
using System.Text;
using Dal.Classes;


namespace Dal.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<BillCategory> BillCategories { get; set; }

    }
}

using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class BillDTO
    {
        public int BillId { get; set; }
        public int UserId { get; set; }
        public string StoreName { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public double? TotalSum { get; set; }
        public string ImgBill { get; set; }
        public string BillTxt { get; set; }

        public virtual ICollection<Category> BillCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

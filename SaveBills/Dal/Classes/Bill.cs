using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes
{
    public partial class Bill
    {
        public Bill()
        {
            Produts = new HashSet<Produt>();
        }
        public Bill(string billText)
        {

        }
        public int BillId { get; set; }
        public int UserId { get; set; }
        public string StoreName { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public double? TotalSum { get; set; }
        public int Category { get; set; }
        public string ImgBiil { get; set; }

        public virtual ICollection<Produt> Produts { get; set; }
    }
}

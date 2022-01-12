using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes
{
    public partial class UserCategory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}

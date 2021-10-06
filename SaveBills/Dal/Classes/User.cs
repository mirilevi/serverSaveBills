using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.Classes
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

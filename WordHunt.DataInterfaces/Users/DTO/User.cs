using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Interfaces.Users.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}

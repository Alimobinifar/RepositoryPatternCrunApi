using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
    }

    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
    }
}

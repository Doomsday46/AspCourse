using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Tournament> Tournaments { get; set; }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   Id == user.Id &&
                   Email == user.Email &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email, Password);
        }
    }
}

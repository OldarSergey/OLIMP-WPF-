using System;
using System.Collections.Generic;
using System.Text;

namespace OLIMP.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public User()
        {
            
        }

        public User(string firstName, string lastName, string middleName, string login, string password, Guid roleId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Login = login;
            Password = password;
            RoleId = roleId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OLIMP.Entities
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
        public Role()
        {

        }
        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}

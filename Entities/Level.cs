using System;
using System.Collections.Generic;
using System.Text;

namespace OLIMP.Entities
{
    public class Level
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Inventory> Inventories { get; set; }

        public Level()
        {
            
        }
        public Level(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}

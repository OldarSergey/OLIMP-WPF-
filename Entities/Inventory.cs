using System;
using System.Collections.Generic;
using System.Text;

namespace OLIMP.Entities
{
    public class Inventory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public decimal Size { get; set; }
        public string Hard { get; set; }
        public decimal Cost { get; set; }

        public List<Contract> Contracts { get; set; }

        public Guid LevelId { get; set; }
        public Guid CategoryId { get; set; }

        public Level Level { get; set; }
        public Category Category { get; set; }

        public Inventory()
        {
            
        }

        public Inventory(string name, string gender, decimal size, string hard, decimal cost, List<Contract> contracts, Guid levelId, Guid categoryId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Gender = gender;
            Size = size;
            Hard = hard;
            Cost = cost;
            Contracts = contracts;
            LevelId = levelId;
            CategoryId = categoryId;
        }
    }
}

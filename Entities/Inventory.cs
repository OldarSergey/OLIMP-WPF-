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

        public override bool Equals(object obj)
        {
            return obj is Inventory inventory &&
                   Id.Equals(inventory.Id) &&
                   Name == inventory.Name &&
                   Gender == inventory.Gender &&
                   Size == inventory.Size &&
                   Hard == inventory.Hard &&
                   Cost == inventory.Cost &&
                   EqualityComparer<List<Contract>>.Default.Equals(Contracts, inventory.Contracts) &&
                   LevelId.Equals(inventory.LevelId) &&
                   CategoryId.Equals(inventory.CategoryId) &&
                   EqualityComparer<Level>.Default.Equals(Level, inventory.Level) &&
                   EqualityComparer<Category>.Default.Equals(Category, inventory.Category);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(Gender);
            hash.Add(Size);
            hash.Add(Hard);
            hash.Add(Cost);
            hash.Add(Contracts);
            hash.Add(LevelId);
            hash.Add(CategoryId);
            hash.Add(Level);
            hash.Add(Category);
            return hash.ToHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OLIMP.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal SumDeposit { get; set; }
        public decimal PriceDay { get; set; }

        public List<Inventory> Inventories { get; set; }

        public Category()
        {
            
        }
        public Category(string name, decimal sumDeposit, decimal priceDay)
        {
            Id = Guid.NewGuid();
            Name = name;
            SumDeposit = sumDeposit;
            PriceDay = priceDay;
        }
    }
}

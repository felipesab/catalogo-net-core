using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemRepository
    {
        private readonly List<Item> Items = new()
        {
            new Item{ Id = new Guid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow},
            new Item{ Id = new Guid(), Name = "Wooden Sword", Price = 15, CreatedDate = DateTimeOffset.UtcNow},
            new Item{ Id = new Guid(), Name = "Wooden Shield", Price = 19, CreatedDate = DateTimeOffset.UtcNow}
        };

        public IEnumerable<Item> GetItems()
        {
            return Items;
        }

        public Item GetItem(Guid id)
        {
            return Items.Where(item => item.Id == id).SingleOrDefault();
        }
    }
}
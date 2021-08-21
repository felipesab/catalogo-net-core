using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IInMemItemRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }

    public class InMemItemRepository : IItemRepository
    {
        private readonly List<Item> Items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Wooden Sword", Price = 15, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Wooden Shield", Price = 19, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems()
        {
            return Items;
        }

        public Item GetItem(Guid id)
        {
            return Items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
            Items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index = Items.FindIndex(existingItem => existingItem.Id == item.Id);
            Items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            var index = Items.FindIndex(item => item.Id == id);
            Items.RemoveAt(index);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(Items);
        }

        public Task<Item> GetItemAsync(Guid id)
        {
            var items = Items.Where(item => item.Id == id).SingleOrDefault();
            return Task.FromResult(items);
        }

        public async Task CreateItemAsync(Item item)
        {
            Items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = Items.FindIndex(existingItem => existingItem.Id == item.Id);
            Items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = Items.FindIndex(item => item.Id == id);
            Items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
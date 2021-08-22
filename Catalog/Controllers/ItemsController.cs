using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _repository;

        public ItemsController(IItemRepository repository) => _repository = repository;

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return _repository.GetItems().Select(item => item.AsDto());
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _repository.GetItem(id).AsDto();

            if(item is null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public ActionResult CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new {Id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = _repository.GetItem(id);
            if(existingItem is null)
            {
                return NotFound();
            }

            var item = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            _repository.UpdateItem(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _repository.GetItem(id);
            if(existingItem is null)
            {
                return NotFound();
            }

            _repository.DeleteItem(id);
            return NoContent();
        }
    }
}
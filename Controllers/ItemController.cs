using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOApi.Interfaces;
using TODOApi.Models;

namespace TODOApi.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemRepository _itemRepository;

        public ItemController(
            ILogger<ItemController> logger,
            IItemRepository itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            try
            {
                var entity = await _itemRepository.GetByIdAsync(id);

                if (entity == null)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Item>> GetItems(
            int? id,
            int? userId,
            DateTime? fromCreateTime,
            DateTime? toCreateTime,
            DateTime? fromDeadline,
            DateTime? toDeadline,
            bool? isDone,
            string? name)
        {
            try
            {
                var list = await _itemRepository.GetAsync(
                    id,
                    userId,
                    fromCreateTime,
                    toCreateTime,
                    fromDeadline,
                    toDeadline,
                    isDone,
                    name);

                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");

                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteItem(int id)
        {
            try
            {
                await _itemRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Item>> PostAsync([FromBody] Item item)
        {
            try
            {
                await _itemRepository.PostAsync(item);
                return Ok(item);
            } 
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task PutAsync([FromBody] Item item)
        {
            try
            {
                await _itemRepository.PutAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task CheckAsync(int id)
        {
            try
            {
                await _itemRepository.CheckAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
        }
    }
}

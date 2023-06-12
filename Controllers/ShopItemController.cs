using Microsoft.AspNetCore.Mvc;
using ShopBridge.AppServices;
using ShopBridge.Model;

namespace ShopBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopItemController : ControllerBase
    {

        private readonly ILogger<ShopItemController> _logger;
        private readonly IShopItemServices _shopItemServices;

        public ShopItemController(ILogger<ShopItemController> logger, IShopItemServices shopItemServices)
        {
            _logger = logger;
            _shopItemServices = shopItemServices;
        }

        [HttpGet]
        [Route("ShopItemById")]
        public async Task<ActionResult<ErrorModel>> GetShopItem([FromQuery] ShopItemByID shopItemByID)
        {

            return await Task.Run(() => _shopItemServices.GetShopItem(shopItemByID.Id));

        }

        [HttpGet]
        [Route("ShopItemByName")]
        public async Task<ActionResult<ErrorModel>> GetShopItem([FromQuery] ShopItemByName shopItemByName)
        {
            return await Task.Run(() => _shopItemServices.GetShopItem(0,shopItemByName.Name));
        }

        [HttpPost]
        [Route("ShopItem")]
        public async Task<ActionResult<ErrorModel>> AddShopItem([FromBody] ShopItemCreat shopItem)
        {
            return await Task.Run(() => _shopItemServices.AddShopItem(shopItem));
        }

        [HttpPut]
        [Route("ShopItem")]
        public async Task<ActionResult<ErrorModel>> UpdateShopItem([FromBody] ShopItem shopItem)
        {
            return await Task.Run(() => _shopItemServices.UpdateShopItem(shopItem));
        }

        [HttpDelete]
        [Route("ShopItem")]
        public async Task<ActionResult<ErrorModel>> RemoveShopItem([FromBody] ShopItemByID shopItemByID)
        {
            return await Task.Run(() => _shopItemServices.RemoveShopItem(shopItemByID.Id));
        }
    }
}
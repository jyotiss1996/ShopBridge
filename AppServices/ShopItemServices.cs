using ShopBridge.Model;
using ShopBridge.Reopsitory;

namespace ShopBridge.AppServices
{
    public class ShopItemServices : IShopItemServices
    {
        private readonly IShopItemRepository _shopItemRepository;
        public ShopItemServices(IShopItemRepository shopItemRepository)
        {
            _shopItemRepository = shopItemRepository;
        }

        public async Task<ErrorModel> GetShopItem(int Id = 0, string Name = "")
        {
            return await Task.Run(() => _shopItemRepository.GetShopItem(Id, Name));
        }

        public async Task<ErrorModel> AddShopItem(ShopItemCreat shopItem)
        {
            return await Task.Run(() => _shopItemRepository.AddShopItem(shopItem));
        }

        public async Task<ErrorModel> UpdateShopItem(ShopItem shopItem)
        {
            return await Task.Run(() => _shopItemRepository.UpdateShopItem(shopItem));
        }

        public async Task<ErrorModel> RemoveShopItem(int Id)
        {
            return await Task.Run(() => _shopItemRepository.RemoveShopItem(Id));
        }
    }
}

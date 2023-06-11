using ShopBridge.Model;

namespace ShopBridge.Reopsitory
{
    public interface IShopItemRepository
    {
        Task<ErrorModel> GetShopItem(int Id = 0, string Name = "");

        Task<ErrorModel> AddShopItem(ShopItemCreat shopItem);
        Task<ErrorModel> UpdateShopItem(ShopItem shopItem);
        Task<ErrorModel> RemoveShopItem(int id);
    }
}

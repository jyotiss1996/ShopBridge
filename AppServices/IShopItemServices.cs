using ShopBridge.Model;

namespace ShopBridge.AppServices
{
    public interface IShopItemServices
    {
        Task<ErrorModel> GetShopItem(int Id = 0, string Name = "");

        Task<ErrorModel> AddShopItem(ShopItemCreat shopItem);
        Task<ErrorModel> UpdateShopItem(ShopItem shopItem);
        Task<ErrorModel> RemoveShopItem(int id);
    }
}

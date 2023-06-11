using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShopBridge.Model
{
    public class ShopItem
    {
        [Required(ErrorMessage = "Id Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Not in Corret Format")]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [RegularExpression("^[0-9]\\d{0,9}(\\.\\d{1,3})?%?$", ErrorMessage = "Not in Corret Format")]
        public decimal? Price { get; set; }
    }

    public class ShopItemCreat
    {
        [JsonIgnore]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [RegularExpression("^[0-9]\\d{0,9}(\\.\\d{1,3})?%?$", ErrorMessage = "Not in Corret Format")]
        public decimal? Price { get; set; }
    }

    public class ShopItemByID
    {
        [Required(ErrorMessage ="Id Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Not in Corret Format")]
        public int Id { get; set; }
    }

    public class ShopItemDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public Int16? Actv_Ind { get; set; }

        public DateTime? Created_Dt { get; set; }
        public DateTime? LastUpdated_Dt { get; set; }
    }
}

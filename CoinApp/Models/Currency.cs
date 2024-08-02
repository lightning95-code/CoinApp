using Newtonsoft.Json;

namespace CoinApp.Models
{
    public class Currency
    {
        [JsonProperty("id")]
        public string Id { get; set; } // Ідентифікатор

        [JsonProperty("rank")]
        public int Rank { get; set; } // Ранг

        [JsonProperty("symbol")]
        public string Symbol { get; set; } // Символ

        [JsonProperty("name")]
        public string Name { get; set; } // Назва

        [JsonProperty("supply")]
        public decimal Supply { get; set; } // Запаси

        [JsonProperty("maxSupply")]
        public decimal? MaxSupply { get; set; } // Максимальна кількість випущених активів (необов'язкове поле)

        [JsonProperty("marketCapUsd")]
        public decimal MarketCapUsd { get; set; } // Ринкова капіталізація в доларах США

        [JsonProperty("volumeUsd24Hr")]
        public decimal VolumeUsd24Hr { get; set; } // Обсяг торгів за останні 24 години в доларах США

        [JsonProperty("priceUsd")]
        public decimal PriceUsd { get; set; } // Актуальна ціна в доларах США

        [JsonProperty("changePercent24Hr")]
        public decimal ChangePercent24Hr { get; set; } // Процентна зміна ціни за останні 24 години

        [JsonProperty("vwap24Hr")]
        public decimal Vwap24Hr { get; set; } // Середньозважена ціна за останні 24 години
    }
}

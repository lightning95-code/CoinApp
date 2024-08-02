using Newtonsoft.Json;

namespace CoinApp.Models
{
    public class HistoricalPriceModel
    {
        [JsonProperty("time")]
        public long Time { get; set; } // Unix-мітка часу

        [JsonProperty("priceUsd")]
        public decimal PriceUsd { get; set; } // Ціна криптовалюти на конкретну дату
    }
}

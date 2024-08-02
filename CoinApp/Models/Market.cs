using Newtonsoft.Json;
using System;

namespace CoinApp.Models
{
    public class Market
    {
        public int RowNumber { get; set; } // Номер рядка

        [JsonProperty("exchangeId")]
        public string? ExchangeId { get; set; } // Ідентифікатор біржі

        [JsonProperty("rank")]
        public int Rank { get; set; } // Ранг

        [JsonProperty("baseSymbol")]
        public string? BaseSymbol { get; set; } // Символ базової криптовалюти

        [JsonProperty("baseId")]
        public string? BaseId { get; set; } // Базова криптовалюта

        [JsonProperty("quoteSymbol")]
        public string? QuoteSymbol { get; set; } // Символ котирувальної валюти

        [JsonProperty("quoteId")]
        public string? QuoteId { get; set; } // Ідентифікатор котирувальної валюти

        [JsonProperty("priceQuote")]
        public decimal? PriceQuote { get; set; } // Ціна базової криптовалюти в котирувальній валюті

        [JsonProperty("priceUsd")]
        public decimal? PriceUsd { get; set; } // Актуальна ціна базової криптовалюти

        [JsonProperty("volumeUsd24Hr")]
        public decimal? VolumeUsd24Hr { get; set; } // Обсяг торгів базовою криптовалютою за останні 24 години

        [JsonProperty("percentExchangeVolume")]
        public decimal? PercentExchangeVolume { get; set; } // Відсоток обсягу торгів на біржі від загального обсягу торгів базовою криптовалютою

        [JsonProperty("tradesCount24Hr")]
        public int? TradesCount24Hr { get; set; } // Кількість угод за останні 24 години

        [JsonProperty("updated")]
        public long? Updated { get; set; } // Скільки часу пройшло з моменту останнього оновлення у мілісекундах


        // Properties for display with default value as "—"
        public string DisplayExchangeId => string.IsNullOrEmpty(ExchangeId) ? "—" : ExchangeId;
        public string DisplayBaseSymbol => string.IsNullOrEmpty(BaseSymbol) ? "—" : BaseSymbol;
        public string DisplayBaseId => string.IsNullOrEmpty(BaseId) ? "—" : BaseId;
        public string DisplayQuoteSymbol => string.IsNullOrEmpty(QuoteSymbol) ? "—" : QuoteSymbol;
        public string DisplayQuoteId => string.IsNullOrEmpty(QuoteId) ? "—" : QuoteId;
        public string DisplayPriceQuote => PriceQuote.HasValue ? PriceQuote.Value.ToString("0.00") : "—";
        public string DisplayPriceUsd => PriceUsd.HasValue ? PriceUsd.Value.ToString("0.00") : "?";
        public string DisplayVolumeUsd24Hr => VolumeUsd24Hr.HasValue ? VolumeUsd24Hr.Value.ToString("0.00") : "?";
        public string DisplayPercentExchangeVolume => PercentExchangeVolume.HasValue ? PercentExchangeVolume.Value.ToString("0.00") : "—";
        public string DisplayTradesCount24Hr => TradesCount24Hr.HasValue ? TradesCount24Hr.Value.ToString() : "—";
    }
}

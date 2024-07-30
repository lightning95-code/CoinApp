using System;

namespace CoinApp.Models
{
    public class Market
    {
        public int RowNumber { get; set; } // Номер рядка
        public string? ExchangeId { get; set; } // Ідентифікатор біржі
        public int Rank { get; set; } // Ранг
        public string? BaseSymbol { get; set; } // Символ базової криптовалюти, наприклад, "BTC"
        public string? BaseId { get; set; } // Базова криптовалюта, наприклад, "bitcoin"
        public string? QuoteSymbol { get; set; } // Символ котирувальної валюти, наприклад, "USD"
        public string? QuoteId { get; set; } // Ідентифікатор котирувальної валюти, наприклад, "united-states-dollar"
        public decimal? PriceQuote { get; set; } // Ціна базової криптовалюти в котирувальній валюті
        public decimal? PriceUsd { get; set; } // Актуальна ціна базової криптовалюти
        public decimal? VolumeUsd24Hr { get; set; } // Обсяг торгів базовою криптовалютою за останні 24 години
        public decimal? PercentExchangeVolume { get; set; } // Відсоток обсягу торгів на біржі від загального обсягу торгів базовою криптовалютою
        public int? TradesCount24Hr { get; set; } // Кількість угод за останні 24 години
        public long? Updated { get; set; } // Скільки часу пройшло з моменту останнього оновлення у мілісекундах


        // Properties for display with default value as "—"
        public string DisplayExchangeId => string.IsNullOrEmpty(ExchangeId) ? "—" : ExchangeId;
        public string DisplayBaseSymbol => string.IsNullOrEmpty(BaseSymbol) ? "—" : BaseSymbol;
        public string DisplayBaseId => string.IsNullOrEmpty(BaseId) ? "—" : BaseId;
        public string DisplayQuoteSymbol => string.IsNullOrEmpty(QuoteSymbol) ? "—" : QuoteSymbol;
        public string DisplayQuoteId => string.IsNullOrEmpty(QuoteId) ? "—" : QuoteId;
        public string DisplayPriceQuote => PriceQuote.HasValue ? PriceQuote.Value.ToString("0.00") : "—";
        public string DisplayPriceUsd => PriceUsd.HasValue ? PriceUsd.Value.ToString("0.00") : "—";
        public string DisplayVolumeUsd24Hr => VolumeUsd24Hr.HasValue ? VolumeUsd24Hr.Value.ToString("0.00") : "—";
        public string DisplayPercentExchangeVolume => PercentExchangeVolume.HasValue ? PercentExchangeVolume.Value.ToString("0.00") : "—";
        public string DisplayTradesCount24Hr => TradesCount24Hr.HasValue ? TradesCount24Hr.Value.ToString() : "—";
    }
}

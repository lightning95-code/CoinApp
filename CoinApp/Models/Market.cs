using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinApp.Models
{
    public class Market
    {
        public string ExchangeId { get; set; }
        public int Rank { get; set; }
        public string BaseSymbol { get; set; } // Символ базової криптовалюти, к примеру, "BTC"
        public string BaseId { get; set; } //  Базова криптовалюта, наприклад, "bitcoin"
        public string QuoteSymbol { get; set; } // Символ котирувальної валюти, наприклад, "USD"
        public string QuoteId { get; set; } // Ідентифікатор котирувальної валюти, наприклад, "united-states-dollar"
        public decimal PriceQuote { get; set; } // Ціна базової криптовалюти в котирувальній валюті
        public decimal PriceUsd { get; set; } // Актуальна ціна базової криптовалюти
        public decimal VolumeUsd24Hr { get; set; }   // Обсяг торгів базовою криптовалютою за останні 24 години
        public decimal PercentExchangeVolume { get; set; } // Відсоток обсягу торгів на біржі від загального обсягу торгів базовою криптовалютою
        public int TradesCount24Hr { get; set; } // Кількість угод за останні 24 години
        public long Updated { get; set; } // Скільки часу пройшло з моменту останнього оновлення у мілісекундах

    }
}

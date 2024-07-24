using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinApp.Models
{
    public class Currency
    {
        public string Id {  get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Supply { get; set; }
        public decimal? MaxSupply { get; set; }
        public decimal MarketCapUsd { get; set; }    // Ринкова капіталізація криптовалюти
        public decimal VolumeUsd24Hr { get; set; }   // Обсяг торгів криптовалюти за останні 24 години
        public decimal PriceUsd { get; set; } // Актуальна ціна криптовалюти
        public decimal ChangePercent24Hr { get; set; } // Процентне змінення ціни криптовалюти за останні 24 години
        public decimal Vwap24Hr { get; set; }  // Середньозважена ціна криптовалюти за останні 24 години

    }
}

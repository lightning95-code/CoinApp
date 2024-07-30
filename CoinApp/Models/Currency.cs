using System;

namespace CoinApp.Models
{
    public class Currency
    {
        public string Id { get; set; } // Ідентифікатор
        public int Rank { get; set; } // Ранг
        public string Symbol { get; set; } // Символ
        public string Name { get; set; } // Назва
        public decimal Supply { get; set; } // Запаси
        public decimal? MaxSupply { get; set; } // Максимальні запаси
        public decimal MarketCapUsd { get; set; } // Ринкова капіталізація в доларах США
        public decimal VolumeUsd24Hr { get; set; } // Обсяг торгів за останні 24 години в доларах США
        public decimal PriceUsd { get; set; } // Актуальна ціна в доларах США
        public decimal ChangePercent24Hr { get; set; } // Процентна зміна ціни за останні 24 години
        public decimal Vwap24Hr { get; set; } // Середньозважена ціна за останні 24 години


    }
}

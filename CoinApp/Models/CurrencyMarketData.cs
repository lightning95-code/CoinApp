using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinApp.Models
{
    // Модель для даних про криптовалюту та ринки (щоб відзначити, на яких ринках торгується валюта)
    public class CurrencyMarketData
    {
        public Currency Currency { get; set; } // Інформація про криптовалюту
        public List<Market> Markets { get; set; } // Список ринків, на яких торгується криптовалюта

        public CurrencyMarketData()
        {
            Markets = new List<Market>();
        }
    }
}

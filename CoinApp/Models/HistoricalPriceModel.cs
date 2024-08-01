using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinApp.Models
{
    public class HistoricalPriceModel
    {
        // Дата, на яку відноситься ціна 
        public long Time { get; set; } // Unix-мітка часу

        // Ціна криптовалюти на конкретну дату
        public decimal PriceUsd { get; set; }
    }
}

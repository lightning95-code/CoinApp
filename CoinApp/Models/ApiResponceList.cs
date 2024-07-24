using Newtonsoft.Json;

namespace CoinApp.Models
{
    public class ApiResponseList<T>
    {
        // Основні дані, повертаємі API (список)
        [JsonProperty("data")]
        public T[] Data { get; set; }

        // Коли було оновленно в останній раз
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}

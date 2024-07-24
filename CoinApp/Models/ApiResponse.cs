using Newtonsoft.Json;

public class ApiResponse<T>
{
    // Основні дані, повертаємі API
    [JsonProperty("data")]
    public T Data { get; set; }  // об'єкт типу T

    // Коли було оновленно в останній раз
    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }
}

using CoinApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;


namespace CoinApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient; // екземпляр HttpClient, що використовується для здійснення HTTP запитів до API

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.coincap.io/"); // Базова адреса запитів
        }

        // Отримання даних про конкретну криптовалюту за ідом
        public async Task<Currency> GetCurrencyDetailsAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v2/assets/{id}"); // Запрос отримання даних про конкретну криптовалюту за ID
                response.EnsureSuccessStatusCode(); // Перевіряє, чи статус-код відповіді є успішним

                var json = await response.Content.ReadAsStringAsync(); // Читає вміст відповіді як рядок (JSON)
                var result = JsonConvert.DeserializeObject<ApiResponse<Currency>>(json); // Десеріалізація, повертаємо дані у ApiResponse<Currency>

                return result.Data;
            }
            catch (HttpRequestException ex)
            {
                // Обробка помилки запиту
                MessageBox.Show($"Error fetching currency details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // Повертаємо null, якщо сталася помилка
            }
            catch (JsonException ex)
            {
                // Обробка помилки десеріалізації
                MessageBox.Show($"Error parsing currency details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // Повертаємо null, якщо сталася помилка
            }
            catch (Exception ex)
            {
                // Обробка інших можливих помилок
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // Повертаємо null, якщо сталася помилка
            }
        }


        // Метод для отримання усіх валют
        public async Task<Currency[]> GetCurrenciesAsync()
        {
            var response = await _httpClient.GetAsync("v2/assets"); // запит отримання даних 
            response.EnsureSuccessStatusCode(); // Перевіряє, чи статус-код відповіді є успішним
            var json = await response.Content.ReadAsStringAsync(); // читає вміст відповіді як рядок (JSON)
            var result = JsonConvert.DeserializeObject<ApiResponseList<Currency>>(json); // Десериализация, повертаємо дані у ApiResponseList<Currency>
            return result.Data;
        }

        // Метод для отримання топових валют
        public async Task<Currency[]> GetTopCurrenciesAsync(int topN = 10)
        {
            var response = await _httpClient.GetAsync("v2/assets?limit=" + topN); // запит отримання даних про топ 10 валют (вони за замовчуванням йдуть по рангу у структурі, що передається)
            response.EnsureSuccessStatusCode(); // Перевіряє, чи статус-код відповіді є успішним
            var json = await response.Content.ReadAsStringAsync(); // читає вміст відповіді як рядок (JSON)
            var result = JsonConvert.DeserializeObject<ApiResponseList<Currency>>(json); // Десериализация, повертаємо дані у ApiResponse<Currency>
            return result.Data;
        }

        // Метод для отримання всіх ринків
        public async Task<Market[]> GetAllMarketsAsync()
        {
            var response = await _httpClient.GetAsync("v2/markets"); // запит отримання даних 
            response.EnsureSuccessStatusCode(); // Перевіряє, чи статус-код відповіді є успішним
            var json = await response.Content.ReadAsStringAsync(); // читає вміст відповіді як рядок (JSON)
            var result = JsonConvert.DeserializeObject<ApiResponseList<Market>>(json); // Десериализация, повертаємо дані у ApiResponseList<Market>
            return result.Data;
        }

        // Метод отримання ринків конкретної валюти
        public async Task<Market[]> GetMarketsForCurrencyAsync(string id)
        {
            var response = await _httpClient.GetAsync($"v2/markets?baseId={id}"); // запит отримання даних за baseId (ід базової валюти)
            response.EnsureSuccessStatusCode(); // Перевіряє, чи статус-код відповіді є успішним
            var json = await response.Content.ReadAsStringAsync(); // читає вміст відповіді як рядок (JSON)
            var result = JsonConvert.DeserializeObject<ApiResponseList<Market>>(json); // Десериализация, повертаємо дані у ApiResponseList<Market>
            return result.Data;
        }

        public async Task<List<HistoricalPriceModel>> GetMonthlyHistoricalPricesAsync(string id, DateTime startTime, DateTime endTime)
        {
            try
            {
                // Перетворення дат на Unix Timestamp в мілісекундах
                long startUnixTime = new DateTimeOffset(startTime).ToUnixTimeMilliseconds();
                long endUnixTime = new DateTimeOffset(endTime).ToUnixTimeMilliseconds();

                // Формування та передача URL з параметрами start і end
                var response = await _httpClient.GetAsync($"v2/assets/{id}/history?interval=d1&start={startUnixTime}&end={endUnixTime}");

                // Перевірка успішності запиту
                response.EnsureSuccessStatusCode();

                // Зчитування вмісту відповіді як рядок (JSON)
                var json = await response.Content.ReadAsStringAsync();

                // Десеріалізація JSON в об'єкти ApiResponseList<HistoricalPriceModel>
                var result = JsonConvert.DeserializeObject<ApiResponseList<HistoricalPriceModel>>(json);

                // Повернення даних як список
                return result.Data.ToList();
            }
            catch (HttpRequestException ex)
            {
                // Handling request error
                MessageBox.Show($"Error fetching historical data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (JsonException ex)
            {
                // Handling JSON parsing error
                MessageBox.Show($"Error parsing data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                // Handling other unexpected errors
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

    }
}

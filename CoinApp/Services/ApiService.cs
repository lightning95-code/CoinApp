using CoinApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


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
            var response = await _httpClient.GetAsync($"v2/assets/{id}"); // запрос отримання даних про конкретну криптовалюту за ідом
            response.EnsureSuccessStatusCode(); // Перевіряє, чи статус-код відповіді є успішним
            var json = await response.Content.ReadAsStringAsync(); // читає вміст відповіді як рядок (JSON)
            var result = JsonConvert.DeserializeObject<ApiResponse<Currency>>(json); // Десериализация, повертаємо дані у ApiResponseList<Currency>
            return result.Data;
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
    }
}

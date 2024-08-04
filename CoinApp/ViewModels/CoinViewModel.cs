using CoinApp.Models;
using CoinApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;

namespace CoinApp.ViewModels
{
    public class CoinViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService; // Сервіс для взаємодії з API
        private string _coinId; // Ідентифікатор криптовалюти

        // Конструктор класу
        public CoinViewModel(string coinId = null)
        {
            _apiService = new ApiService(); // Ініціалізація сервісу
            _coinId = coinId; // Присвоєння ідентифікатора криптовалюти
            LoadCoinData(); // Завантаження даних про криптовалюту
        }

        // Збереження даних про криптовалюту
        private Currency _coin;
        public Currency Coin
        {
            get => _coin;
            private set
            {
                _coin = value; // Присвоєння нового значення
                OnPropertyChanged(); // Оновлення прив'язаних елементів
            }
        }

        // Дані для графіка
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            private set
            {
                _seriesCollection = value; // Присвоєння нових даних для графіка
                OnPropertyChanged(); // Оновлення прив'язаних елементів
            }
        }

        private Axis _xAxis;
        public Axis XAxis
        {
            get => _xAxis;
            private set
            {
                _xAxis = value; // Присвоєння нових даних для осі X
                OnPropertyChanged(); // Оновлення прив'язаних елементів
            }
        }

        private Axis _yAxis;
        public Axis YAxis
        {
            get => _yAxis;
            private set
            {
                _yAxis = value; // Присвоєння нових даних для осі Y
                OnPropertyChanged(); // Оновлення прив'язаних елементів
            }
        }

        // Метод для завантаження історичних цін
        private async Task LoadHistoricalPricesAsync(string coinId)
        {
            try
            {
                // Визначення початкової і кінцевої дати для одного місяця
                DateTime endTime = DateTime.UtcNow; // Поточний час
                DateTime startTime = endTime.AddMonths(-1); // Один місяць назад

                // Отримання історичних даних про ціну
                var historicalPrices = await _apiService.GetMonthlyHistoricalPricesAsync(coinId, startTime, endTime);

                if (historicalPrices != null && historicalPrices.Any())
                {
                    // Перетворення даних на масиви значень і дат
                    var values = historicalPrices.Select(p => (double)p.PriceUsd).ToArray();
                    var dates = historicalPrices.Select(p => UnixTimeStampToDateTime(p.Time).ToString("dd/MM/yyyy")).ToArray();

                    // Налаштування серії даних для графіка
                    SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Price", // Заголовок серії даних
                    Values = new ChartValues<double>(values) // Значення для графіка
                }
            };

                    // Налаштування осі X
                    XAxis = new Axis
                    {
                        Title = "Date", // Заголовок осі X
                        Labels = dates // Мітки осі X
                    };

                    // Налаштування осі Y
                    YAxis = new Axis
                    {
                        Title = "Price", // Заголовок осі Y
                        LabelFormatter = value => value.ToString("C", new CultureInfo("en-US"))//щоб у доларах
                    };
                }
                else
                {
                    // Якщо даних немає, встановити порожню серію
                    SeriesCollection = new SeriesCollection();
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок
                MessageBox.Show($"Error loading historical prices: {ex.Message}");
            }
        }

        // Метод для завантаження конкретної валюти
        private async Task LoadCoinDataAsync(string coinId)
        {
            try
            {
                // Отримання даних про конкретну валюту
                Coin = await _apiService.GetCurrencyDetailsAsync(coinId);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                MessageBox.Show($"Error loading coin data: {ex.Message}");
            }
        }

        // Метод для завантаження даних валюти за замовчуванням
        private async void LoadInitialCoinData()
        {
            try
            {
                // Отримання списку всіх валют
                var currencies = await _apiService.GetTopCurrenciesAsync();

                if (currencies != null && currencies.Length > 0)
                {
                    // Отримання ідентифікатора першої валюти в списку
                    string firstCoinId = currencies[0].Id;
                    await LoadCoinDataAsync(firstCoinId); // Завантаження даних для першої валюти
                    await LoadHistoricalPricesAsync(firstCoinId);
                }
                else
                {
                    Coin = null; // Якщо немає валют, очищення даних
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок
                MessageBox.Show($"Error loading initial coin data: {ex.Message}");
            }
        }

        // Метод для завантаження даних валюти
        private async void LoadCoinData()
        {
            if (string.IsNullOrEmpty(_coinId))
            {
                // Якщо ідентифікатор валюти не вказаний, завантажити дані за замовчуванням
                LoadInitialCoinData();
            }
            else
            {
                // Якщо ідентифікатор валюти вказаний, завантажити дані для цієї валюти
                await LoadCoinDataAsync(_coinId);
                await LoadHistoricalPricesAsync(_coinId);
            }      
        }

        // Метод для перетворення Unix-міток у DateTime
        public DateTime UnixTimeStampToDateTime(long unixTimeStampMillis)
        {
            // Unix timestamp - це кількість мілісекунд, що пройшли з початкової дати
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Початкова дата (Epoch)
            return epoch.AddMilliseconds(unixTimeStampMillis).ToLocalTime(); // Додавання мілісекунд до Epoch і конвертація у локальний час
        }

        public void Refresh_data()
        {
            LoadCoinData();
        }

        public async Task<string> GetFirstCurrencyIdAsync()
        {
            var currencies = await _apiService.GetTopCurrenciesAsync();
            return currencies != null && currencies.Length > 0 ? currencies[0].Id : null;
        }

        // Подія для сповіщення про зміни властивостей
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Сповіщення про зміну властивості
        }
    }
}

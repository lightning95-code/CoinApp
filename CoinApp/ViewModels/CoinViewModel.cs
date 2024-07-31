using CoinApp.Models;
using CoinApp.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace CoinApp.ViewModels
{
    public class CoinViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private string _coinId; // Изменено на private и добавлено поле для хранения ID валюты

        public CoinViewModel(string coinId = null)
        {
            _apiService = new ApiService();
            _coinId = coinId; // Установка coinId через конструктор
            LoadCoinData();
        }

        // Збереження даних про криптовалюту
        private Currency _coin;
        public Currency Coin
        {
            get => _coin;
            private set
            {
                _coin = value;
                OnPropertyChanged();
            }
        }

        // Метод для завантаження даних конкретної валюти
        private async Task LoadCoinDataAsync(string coinId)
        {
            try
            {
                Coin = await _apiService.GetCurrencyDetailsAsync(coinId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading coin data: {ex.Message}");
            }
        }

        // Метод для завантаження за замовчуванням
        private async void LoadInitialCoinData()
        {
            try
            {
                // Завантаження усіх валют
                var currencies = await _apiService.GetCurrenciesAsync();

                if (currencies != null && currencies.Length > 0)
                {
                    // Отримання даних першої валюти зі списку
                    string firstCoinId = currencies[0].Id;
                    await LoadCoinDataAsync(firstCoinId);
                }
                else
                {
                    Coin = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial coin data: {ex.Message}");
            }
        }

        private async void LoadCoinData()
        {
            if (string.IsNullOrEmpty(_coinId))
            {
                LoadInitialCoinData();
            }
            else
            {
                await LoadCoinDataAsync(_coinId); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

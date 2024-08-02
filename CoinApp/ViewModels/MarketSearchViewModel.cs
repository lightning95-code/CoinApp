using CoinApp.Models;
using CoinApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace CoinApp.ViewModels
{
    public class MarketSearchViewModel : INotifyPropertyChanged
    {
        private string _coinId; // Ідентифікатор криптовалюти

        private readonly ApiService _apiService;
        private ObservableCollection<Market> _markets;

        public ObservableCollection<Market> Markets
        {
            get => _markets;
            set
            {
                if (_markets != value)
                {
                    _markets = value;
                    OnPropertyChanged(nameof(Markets));
                }
            }
        }

        public MarketSearchViewModel(string coinId = null)
        {
            _apiService = new ApiService();
            _coinId = coinId;
            LoadMarkets(); // Завантаження даних за замовчуванням
        }

        private async void LoadMarkets()
        {
            if (string.IsNullOrEmpty(_coinId))
            {
                // Якщо ідентифікатор валюти не вказаний, завантажити дані за замовчуванням
                LoadMarketsForFirstCoin();
            }
            else
            {
                // Якщо ідентифікатор валюти вказаний, завантажити дані для цієї валюти
                await LoadMarketsForCoin(_coinId);
            }
  
        }

        private async Task LoadMarketsForCoin(string coin_id)
        {
            try
            {
                // Отримання даних про ринки для конкретної валюти
                CurrencyMarketData currencyMarketData = await _apiService.GetMarketsForCurrencyAsync(coin_id);

                // встановлюємо номера 
                int rowNumber = 1;
                foreach (var market in currencyMarketData.Markets)
                {
                    market.RowNumber = rowNumber++;
                }

                // Оновлення колекції ринків
                Markets = new ObservableCollection<Market>(currencyMarketData.Markets);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                MessageBox.Show($"Error loading markets data: {ex.Message}");
            }
        }

        private async void LoadMarketsForFirstCoin()
        {
            try
            {
                // Отримання списку всіх валют
                var currencies = await _apiService.GetTopCurrenciesAsync();

                if (currencies != null && currencies.Length > 0)
                {
                    // Отримання ідентифікатора першої валюти в списку
                    string firstCoinId = currencies[0].Id;
                    await LoadMarketsForCoin(firstCoinId); // Завантаження даних для першої валюти

                    // Заповнення ComboBox або іншого елемента інтерфейсу

                }
                else
                {
                    // Відображення повідомлення про відсутність даних
                    MessageBox.Show("No currencies available.");
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок
                MessageBox.Show($"Error loading initial data: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using CoinApp.Models;
using CoinApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoinApp.ViewModels
{
    public class MarketSearchViewModel : INotifyPropertyChanged
    {
        private string _coinId; // Ідентифікатор криптовалюти
        private readonly ApiService _apiService;
        private ObservableCollection<Market> _markets;
        private ObservableCollection<string> _currencyNames; // Список назв валют
        private string _selectedCurrencyName; // Обрана валюта

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

        public ObservableCollection<string> CurrencyNames
        {
            get => _currencyNames;
            set
            {
                if (_currencyNames != value)
                {
                    _currencyNames = value;
                    OnPropertyChanged(nameof(CurrencyNames));
                }
            }
        }

        public string SelectedCurrencyName
        {
            get => _selectedCurrencyName;
            set
            {
                if (_selectedCurrencyName != value)
                {
                    _selectedCurrencyName = value;
                    OnPropertyChanged(nameof(SelectedCurrencyName));
                    LoadMarketsForSelectedCurrency();
                }
            }
        }

        public MarketSearchViewModel(string coinId)
        {
            _apiService = new ApiService();
            _coinId = coinId;
            LoadMarkets(); // Завантаження даних за замовчуванням
            LoadCurrencyNames(); // Завантаження назв валют
        }

        private async void LoadMarketsForSelectedCurrency()
        {
            if (!string.IsNullOrEmpty(SelectedCurrencyName))
            {
                try
                {
                    var currencies = await _apiService.GetCurrenciesAsync();
                    var selectedCurrency = currencies.FirstOrDefault(c => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(c.Id) == SelectedCurrencyName);
                    if (selectedCurrency != null)
                    {
                        await LoadMarketsForCoin(selectedCurrency.Id);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load markets for selected currency: {ex.Message}");
                }
            }
        }

        private async void LoadMarkets()
        {
            if (string.IsNullOrEmpty(_coinId))
            {
                try
                {
                    var currencies = await _apiService.GetTopCurrenciesAsync();
                    if (currencies != null && currencies.Length > 0)
                    {
                        _coinId = currencies[0].Id;
                    }
                    else
                    {
                        MessageBox.Show("No currencies available.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading initial data: {ex.Message}");
                }
            }
            SelectedCurrencyName = !string.IsNullOrEmpty(_coinId) ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_coinId.ToLower()) : null;
            try
            {
                await LoadMarketsForCoin(_coinId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading markets data: {ex.Message}");
            }
        }

        private async Task LoadMarketsForCoin(string coinId)
        {
            try
            {
                var currencyMarketData = await _apiService.GetMarketsForCurrencyAsync(coinId);
                int rowNumber = 1;
                foreach (var market in currencyMarketData.Markets)
                {
                    market.RowNumber = rowNumber++;
                }
                Markets = new ObservableCollection<Market>(currencyMarketData.Markets);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading markets data: {ex.Message}");
            }
        }

        private async void LoadCurrencyNames()
        {
            try
            {
                var currencies = await _apiService.GetCurrenciesAsync();
                CurrencyNames = new ObservableCollection<string>(
                    currencies.Select(c => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(c.Id))
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load currency names: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

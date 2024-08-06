using CoinApp.Models;
using CoinApp.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoinApp.ViewModels
{
    public class ConvertViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _currencyNames = new ObservableCollection<string>();
        private readonly ApiService _apiService;
        private string _firstSelectedCurrencyName;
        private string _secondSelectedCurrencyName;

        private decimal? _exchangeRate;
        private decimal? _amount = 1;

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

        public string FirstSelectedCurrencyName
        {
            get => _firstSelectedCurrencyName;
            set
            {
                if (_firstSelectedCurrencyName != value)
                {
                    _firstSelectedCurrencyName = value;
                    OnPropertyChanged(nameof(FirstSelectedCurrencyName));
                }
            }
        }

        public string SecondSelectedCurrencyName
        {
            get => _secondSelectedCurrencyName;
            set
            {
                if (_secondSelectedCurrencyName != value)
                {
                    _secondSelectedCurrencyName = value;
                    OnPropertyChanged(nameof(SecondSelectedCurrencyName));
                }
            }
        }

        public decimal? ExchangeRate
        {
            get => _exchangeRate;
            set
            {
                if (_exchangeRate != value)
                {
                    _exchangeRate = value;
                    OnPropertyChanged(nameof(ExchangeRate));
                }
            }
        }
    

        public ConvertViewModel()
        {
            _apiService = new ApiService();
            LoadCurrencyNames();
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

        public async Task<decimal?> GetExchangeRateAsync(string fromCurrencyId, string toCurrencyId)
        {
            try
            {
                var fromCurrency = await _apiService.GetCurrencyDetailsAsync(fromCurrencyId.ToLower());
                var toCurrency = await _apiService.GetCurrencyDetailsAsync(toCurrencyId.ToLower());

                if (fromCurrency == null || toCurrency == null)
                {
                    return null;
                }

                // Рассчитайте обменный курс
                return fromCurrency.PriceUsd / toCurrency.PriceUsd;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to get exchange rate: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public async Task UpdateExchangeRateAsync()
        {
            if (string.IsNullOrEmpty(FirstSelectedCurrencyName) || string.IsNullOrEmpty(SecondSelectedCurrencyName))
            {
                ExchangeRate = null;
                return;
            }

            ExchangeRate = await GetExchangeRateAsync(FirstSelectedCurrencyName, SecondSelectedCurrencyName);

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using CoinApp.Models;
using CoinApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace CoinApp.ViewModels
{
    public class CurrenciesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Currency> _currencies;
        private readonly ApiService _apiService;

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set
            {
                if (_currencies != value)
                {
                    _currencies = value;
                    OnPropertyChanged(nameof(Currencies));
                }
            }
        }

        public CurrenciesViewModel()
        {
            _apiService = new ApiService();
            LoadTopCurrencies();
        }

        private async void LoadTopCurrencies()
        {
            try
            {
                var topCurrencies = await _apiService.GetTopCurrenciesAsync();
                Currencies = new ObservableCollection<Currency>(topCurrencies);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading currencies: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using CoinApp.Models;
using CoinApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace CoinApp.ViewModels
{
    public class MarketsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Market> _markets;

        private readonly ApiService _apiService;

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

        private async Task LoadMarkets()
        {
            try
            {
                var allMarkets = await _apiService.GetAllMarketsAsync();

                // встановлюємо номера 
                int rowNumber = 1;
                foreach (var market in allMarkets)
                {
                    market.RowNumber = rowNumber++;
                }

                Markets = new ObservableCollection<Market>(allMarkets);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading all markets: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task RefreshDataAsync()
        {
            await LoadMarkets();
        }

        public MarketsViewModel()
        {
            _apiService = new ApiService();
            LoadMarkets(); //за замовчуванням
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}

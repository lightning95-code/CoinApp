using CoinApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoinApp.Views
{
    /// <summary>
    /// Interaction logic for MarketSearchView.xaml
    /// </summary>
    public partial class MarketSearchView : Window
    {
        private bool IsMaximized = false; // Максимізація вікна
        private string _coinId;

        private MarketSearchViewModel _viewModel;
        public MarketSearchView(string coinId)
        {
            InitializeComponent();
            _coinId = coinId;

            _viewModel = new MarketSearchViewModel(_coinId);
            DataContext = _viewModel; //встановлюю дата контекст для вікна
        }

        private async void Refresh_Button_Click(object sender, RoutedEventArgs e) {

        }
        
        // Максимізація вікна
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        // Переміщення вікна при зажатій ЛКМ
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}

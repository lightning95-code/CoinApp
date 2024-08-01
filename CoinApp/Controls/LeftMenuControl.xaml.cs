using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CoinApp.Utilities;
using CoinApp.Views;

namespace CoinApp.Controls
{
    public partial class LeftMenuControl : UserControl
    {
        public LeftMenuControl()
        {
            InitializeComponent();
        }

        // перехід до головного вікна
        private async void Main_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (window == null)
            {
                MessageBox.Show("Window not found."); // Обробка випадку, коли вікно не знайдене
                return;
            }

            // Збереження стану вікна
            WindowStateManager.Width = window.Width;
            WindowStateManager.Height = window.Height;
            WindowStateManager.Top = window.Top;
            WindowStateManager.Left = window.Left;
            WindowStateManager.IsMaximized = window.WindowState == WindowState.Maximized;

            MainWindow mainWin = new MainWindow
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            mainWin.Show();

            // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
            await Task.Delay(100);

            window.Close(); // Закриття поточного вікна
        }

        //перехід до CoinView
        private async void Coin_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (window == null)
            {
                MessageBox.Show("Window not found."); // Обробка випадку, коли вікно не знайдене
                return;
            }

            // Збереження стану вікна
            WindowStateManager.Width = window.Width;
            WindowStateManager.Height = window.Height;
            WindowStateManager.Top = window.Top;
            WindowStateManager.Left = window.Left;
            WindowStateManager.IsMaximized = window.WindowState == WindowState.Maximized;

            CoinView coinWin = new CoinView(null)
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            coinWin.Show();

            // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
            await Task.Delay(100);

            window.Close(); // Закриття поточного вікна
        }

        // Перехід до MarketView
        private async void Markets_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (window == null)
            {
                MessageBox.Show("Window not found."); // Обробка випадку, коли вікно не знайдене
                return;
            }

            // Збереження стану вікна
            WindowStateManager.Width = window.Width;
            WindowStateManager.Height = window.Height;
            WindowStateManager.Top = window.Top;
            WindowStateManager.Left = window.Left;
            WindowStateManager.IsMaximized = window.WindowState == WindowState.Maximized;

            MarketsView marketsView = new MarketsView
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            marketsView.Show();

            // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
            await Task.Delay(100);

            window.Close(); // Закриття поточного вікна
        }
    }
}

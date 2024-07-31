using CoinApp.Utilities;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CoinApp.Views
{
    /// <summary>
    /// Interaction logic for CoinView.xaml
    /// </summary>
    public partial class CoinView : Window
    {

        private bool IsMaximized = false; //максимізація вікна
        private string _coinId;

        private CoinViewModel _viewModel;
        public CoinView(string coinId)
        {
            InitializeComponent();
            _coinId = coinId;

            _viewModel = new CoinViewModel(coinId);
            DataContext = _viewModel; //встановлюю дата контекст для вікна
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Main_Button_Click(object sender, RoutedEventArgs e)
        {
            // Збереження стану вікна
            WindowStateManager.Width = this.Width;
            WindowStateManager.Height = this.Height;
            WindowStateManager.Top = this.Top;
            WindowStateManager.Left = this.Left;
            WindowStateManager.IsMaximized = this.WindowState == WindowState.Maximized;

            MainWindow main_win = new MainWindow
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            main_win.Show();

            this.Close();
        }

        private void Markets_Button_Click(object sender, RoutedEventArgs e)
        {
            // Збереження стану вікна
            WindowStateManager.Width = this.Width;
            WindowStateManager.Height = this.Height;
            WindowStateManager.Top = this.Top;
            WindowStateManager.Left = this.Left;
            WindowStateManager.IsMaximized = this.WindowState == WindowState.Maximized;

            MarketsView marketsView = new MarketsView
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            marketsView.Show();

            this.Close();
        }

        //вихід із програми
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            // Запуск анімації скриття
            DoubleAnimation hideAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            // Додаю анімацію до цього вікна
            this.BeginAnimation(UIElement.OpacityProperty, hideAnimation);

            // Затримка перед завершенням роботи
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5) // тривалість анімації
            };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                Application.Current.Shutdown();
            };
            timer.Start();
        }

        //Максимізація вікна
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

        //Переміщення вікна при зажатій ЛКМ
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Coin_Button_Click(object sender, RoutedEventArgs e)
        {
            // Збереження стану вікна
            WindowStateManager.Width = this.Width;
            WindowStateManager.Height = this.Height;
            WindowStateManager.Top = this.Top;
            WindowStateManager.Left = this.Left;
            WindowStateManager.IsMaximized = this.WindowState == WindowState.Maximized;

            CoinView coin_win = new CoinView(null)
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            coin_win.Show();

            this.Close();
        }

    }
}

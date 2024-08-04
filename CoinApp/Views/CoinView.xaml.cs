﻿using CoinApp.Utilities;
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
using static MaterialDesignThemes.Wpf.Theme;

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

            _viewModel = new CoinViewModel(_coinId);
            DataContext = _viewModel; //встановлюю дата контекст для вікна
        }

        private async void Go_To_Market_Search_Page(object sender, RoutedEventArgs e)
        {
            string currencyId = _coinId;

            if (string.IsNullOrEmpty(currencyId))
            {
                // Отримання ID першої валюти з ViewModel, якщо ID валюти не вказано
                currencyId = await _viewModel.GetFirstCurrencyIdAsync();
            }

            if (!string.IsNullOrEmpty(currencyId))
            {
                // Збереження стану поточного вікна
                WindowStateManager.Width = this.Width;
                WindowStateManager.Height = this.Height;
                WindowStateManager.Top = this.Top;
                WindowStateManager.Left = this.Left;
                WindowStateManager.IsMaximized = this.WindowState == WindowState.Maximized;

                // Створення нового вікна для перегляду детальної інформації про валюту
                MarketSearchView MarketSearchWin = new MarketSearchView(currencyId)
                {
                    Width = WindowStateManager.Width,
                    Height = WindowStateManager.Height,
                    Top = WindowStateManager.Top,
                    Left = WindowStateManager.Left,
                    WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
                };

                MarketSearchWin.Show();

                // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
                await Task.Delay(100);

                // Закриття поточного вікна
                this.Close();
            }
            else
            {
                // Виводимо повідомлення, якщо ID валюти не знайдено
                MessageBox.Show("No currency ID found.");
            }
        }

        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel!=null)
            {
                // Робимо видимою панель завантаження
                LoadingPanel.Visibility = Visibility.Visible;
                ChartContainer.Visibility = Visibility.Collapsed;
                CoinDescriptionContainer.Visibility = Visibility.Collapsed;

                _viewModel.Refresh_data();

                await Task.Delay(2500); // Затримка для демонстрації завантаження

                // Приховуємо панель завантаження
                LoadingPanel.Visibility = Visibility.Collapsed;
                CoinDescriptionContainer.Visibility=Visibility.Visible;
                ChartContainer.Visibility = Visibility.Visible;
            }
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

    }
}

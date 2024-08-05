﻿using CoinApp.ViewModels;
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

        private MarketSearchViewModel _viewModel;
        public MarketSearchView(string coinId)
        {
            InitializeComponent();
            _viewModel = new MarketSearchViewModel(coinId);
            DataContext = _viewModel;
        }

        private void CurrencyComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CurrencyComboBox != null && CurrencyComboBox.Text == "Select coin")
            {
                CurrencyComboBox.Text = "";      
            }
            CurrencyComboBox.IsDropDownOpen = true;
        }

        private void CurrencyComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CurrencyComboBox == null || CurrencyComboBox.Text == "")
            {
                CurrencyComboBox.Text = "Select coin";
            }
            CurrencyComboBox.IsDropDownOpen = false;
        }

        // Оновлення вікна
        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            string actual_coin_id = CurrencyComboBox.SelectedItem as string;
            if (_viewModel != null)
            {
                // Робимо видимою панель завантаження, а таблицю навпаки приховуємо
                LoadingPanel.Visibility = Visibility.Visible;
                MarketsSearchDataGrid.Visibility = Visibility.Collapsed;

                await _viewModel.RefreshDataAsync(actual_coin_id.ToLower());

                await Task.Delay(2500);

                // Робимо видимою таблицю, а панель навпаки приховуємо
                LoadingPanel.Visibility = Visibility.Collapsed;
                MarketsSearchDataGrid.Visibility = Visibility.Visible;
            }
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
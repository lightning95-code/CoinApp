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
    /// Interaction logic for ConvertView.xaml
    /// </summary>
    public partial class ConvertView : Window
    {
        private bool IsMaximized = false; // Максимізація вікна

        private ConvertViewModel _viewModel;
        public ConvertView()
        {
            InitializeComponent();
            _viewModel = new ConvertViewModel();
            DataContext = _viewModel;
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

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                comboBox.IsDropDownOpen = true;
            }
            else
            {
                MessageBox.Show($"{sender} is not a ComboBox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                comboBox.IsDropDownOpen = false;
            }
            else
            {
                MessageBox.Show($"{sender} is not a ComboBox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        // Оновлення вікна
        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Get_Exchange_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_viewModel.FirstSelectedCurrencyName) ||
                 string.IsNullOrEmpty(_viewModel.SecondSelectedCurrencyName))
            {
                MessageBox.Show("Please select both currencies.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}

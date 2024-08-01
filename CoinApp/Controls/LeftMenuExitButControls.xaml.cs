using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CoinApp.Controls
{
    /// <summary>
    /// Interaction logic for LeftMenuExitButControls.xaml
    /// </summary>
    public partial class LeftMenuExitButControls : UserControl
    {
        public LeftMenuExitButControls()
        {
            InitializeComponent();
        }

        // Вихід із програми
        private async void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            // Запуск анімації скриття
            DoubleAnimation hideAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.5) // Тривалість анімації
            };

            // Створення асинхронної задачі, яка завершиться по закінченню анімації
            TaskCompletionSource<bool> animationCompletion = new TaskCompletionSource<bool>();

            // Обробник завершення анімації
            hideAnimation.Completed += (s, a) => animationCompletion.SetResult(true);

            // Запуск анімації на поточному вікні
            this.BeginAnimation(UIElement.OpacityProperty, hideAnimation);

            // Очікування завершення анімації
            await animationCompletion.Task;
            await Task.Delay(150);
            // Завершення роботи програми після закінчення анімації
            Application.Current.Shutdown();
        }

    }
}

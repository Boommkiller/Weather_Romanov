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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Weather.Classes;
using Weather.Models;

namespace Weather
{
    public partial class MainWindow : Window
    {
        public double Latitude = 58.01;
        public double Longitude = 56.25;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await LoadWeather();
        }

        private async void RefreshWeather(object sender, RoutedEventArgs e)
        {
            await LoadWeather();
        }

        public async Task LoadWeather()
        {
            try
            {
                refresh.IsEnabled = false;
                status.Text = "Загрузка погоды...";
                List<WeatherRow> rows = await WeatherApi.GetForecast(Latitude, Longitude);
                weatherGrid.ItemsSource = rows;
                status.Text = $"Пермь ({Latitude:0.00}; {Longitude:0.00}). Обновлено: {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                status.Text = "Ошибка: " + ex.Message;
            }
            finally
            {
                refresh.IsEnabled = true;
            }
        }
    }
}

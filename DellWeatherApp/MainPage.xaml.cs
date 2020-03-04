using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DellWeatherApp.JsonParsing;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DellWeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new CityViewModel();
        }

        public CityViewModel ViewModel { get; set; }

        /// <summary>
        /// Refresh button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void CityBtn_Click(object sender, RoutedEventArgs e)
        {
            int cityId = (int)((Button)sender).Tag;
            //Debug.WriteLine($"{cityId}");
            WeatherModel wm = await ViewModel.GetWeather(cityId);

            if (wm != null)
            {
                ViewModel.ShowWeatherInfo(cityId);
                CityNameTxt.Text = ViewModel.ActiveCity.Name;
                SetInfoPanelVisibile();
                UpdateWeatherDetails();
            }

            else
            {
                CityNameTxt.Text = "Something went wrong getting weather information";
                SetInfoPanelCollapsed();
            }
        }

        private void UpdateWeatherDetails()
        {
            TempTxt.Text = ViewModel.ActiveWeather.Main.Temp.ToString();
            LowTempTxt.Text = ViewModel.ActiveWeather.Main.TempMin.ToString();
            HighTempTxt.Text = ViewModel.ActiveWeather.Main.TempMax.ToString();
            SunriseTxt.Text = ViewModel.ActiveWeather.Sys.Sunrise.ToString("HH:mm");
            SunsetTxt.Text = ViewModel.ActiveWeather.Sys.Sunset.ToString("HH:mm");
            HumidityTxt.Text = ViewModel.ActiveWeather.Main.Humidity + "%";
        }

        private void SetInfoPanelVisibile()
        {
            TempTxt.Visibility = Visibility.Visible;
            LowTemp.Visibility = Visibility.Visible;
            LowTempTxt.Visibility = Visibility.Visible;
            HighTemp.Visibility = Visibility.Visible;
            HighTempTxt.Visibility = Visibility.Visible;
            Sunrise.Visibility = Visibility.Visible;
            SunriseTxt.Visibility = Visibility.Visible;
            Sunset.Visibility = Visibility.Visible;
            SunsetTxt.Visibility = Visibility.Visible;
            Humidity.Visibility = Visibility.Visible;
            HumidityTxt.Visibility = Visibility.Visible;
        }

        private void SetInfoPanelCollapsed()
        {
            TempTxt.Visibility = Visibility.Collapsed;
            LowTemp.Visibility = Visibility.Collapsed;
            LowTempTxt.Visibility = Visibility.Collapsed;
            HighTemp.Visibility = Visibility.Collapsed;
            HighTempTxt.Visibility = Visibility.Collapsed;
            Sunrise.Visibility = Visibility.Collapsed;
            SunriseTxt.Visibility = Visibility.Collapsed;
            Sunset.Visibility = Visibility.Collapsed;
            SunsetTxt.Visibility = Visibility.Collapsed;
            Humidity.Visibility = Visibility.Collapsed;
            HumidityTxt.Visibility = Visibility.Collapsed;
        }
    }
}

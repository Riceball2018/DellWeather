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
            SetInfoPanelCollapsed();
        }

        public CityViewModel ViewModel { get; set; }

        /// <summary>
        /// Refresh button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            WeatherModel wm = await ViewModel.GetWeather();

            if (wm != null)
            {
                SetInfoPanelVisibile();
                UpdateWeatherDetails();
            }

            else
            {
                CityNameTxt.Text = "Refresh failed";
                SetInfoPanelCollapsed();
            }
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
            float temp, lowTemp, highTemp;
            string dateFormat = "HH:mm";
            string timeStampFormat = "dd MMMM yyyy HH:mm:ss";
            string tempUnit = " C";

            // Celsius
            if (!TempTgl.IsOn)
            {
                temp = ViewModel.KelvinToC(ViewModel.ActiveWeather.Main.Temp);
                lowTemp = ViewModel.KelvinToC(ViewModel.ActiveWeather.Main.Temp);
                highTemp = ViewModel.KelvinToC(ViewModel.ActiveWeather.Main.Temp);
            }

            // Fahrenheit
            else
            {
                temp = ViewModel.KelvinToF(ViewModel.ActiveWeather.Main.Temp);
                lowTemp = ViewModel.KelvinToF(ViewModel.ActiveWeather.Main.Temp);
                highTemp = ViewModel.KelvinToF(ViewModel.ActiveWeather.Main.Temp);
                tempUnit = " F";
            }

            if (TimeTgl.IsOn)
            {
                dateFormat = "hh:mm tt";
                timeStampFormat = "dd MMMM yyyy hh:mm:ss tt";
            }

            TempTxt.Text = temp.ToString("0.0") + tempUnit;
            LowTempTxt.Text = lowTemp.ToString("0.0") + tempUnit;
            HighTempTxt.Text = highTemp.ToString("0.0") + tempUnit;
            SunriseTxt.Text = ViewModel.ActiveWeather.Sys.Sunrise.ToString(dateFormat);
            SunsetTxt.Text = ViewModel.ActiveWeather.Sys.Sunset.ToString(dateFormat);
            HumidityTxt.Text = ViewModel.ActiveWeather.Main.Humidity + "%";
            TimeStampTxt.Text = ViewModel.ActiveWeather.Dt.ToString(timeStampFormat);
        }

        private void SetInfoPanelVisibile()
        {
            SettingsPanel.Visibility = Visibility.Visible;
            InfoPanel.Visibility = Visibility.Visible;
        }

        private void SetInfoPanelCollapsed()
        {
            SettingsPanel.Visibility = Visibility.Collapsed;
            InfoPanel.Visibility = Visibility.Collapsed;
        }

        private void TempTgl_Toggled(object sender, RoutedEventArgs e)
        {
            UpdateWeatherDetails();
        }

        private void TimeTgl_Toggled(object sender, RoutedEventArgs e)
        {
            UpdateWeatherDetails();
        }
    }
}

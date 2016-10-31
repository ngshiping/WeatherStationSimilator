using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherStationSimilator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        AzureIoTHelper iothelper = new AzureIoTHelper();

        public MainPage()
        {
            this.InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            
            iothelper.MessageReceivedEvent += iothelper_MessageReceivedEvent;
            iothelper.ReceiveDataFromAzure();
        }
        
        bool isStarted = false;
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isStarted)
            {
                isStarted = true;
                timer.Start();
                startBtn.Content = "Stop";
            }
            else
            {
                isStarted = false;
                timer.Stop();
                startBtn.Content = "Start";
            }
        }

        private async void timer_Tick(object sender, object e)
        {
            await getWeatherData();
        }

        private async Task getWeatherData()
        {
            // purely to simulate the weather data ONLY!
            // No one randomize the weather info like this

            Weather weather = new Weather()
            {
                deviceID = AzureIoTHelper.devicename,
                temperature = WeatherRandomizer.RandomizeTemperatureValue(),
                humidity = WeatherRandomizer.RandomizeHumidityValue(),
                pressure = WeatherRandomizer.RandomizePressureValue(),
                createdAt = DateTime.UtcNow.Truncate(TimeSpan.FromMinutes(1))
            };

            // Just to show the data on the screen
            temperatureTB.Text = weather.temperature.ToString("0.0 C");
            humidityTB.Text = weather.humidity.ToString("0") + " %";
            pressureTB.Text = weather.pressure.ToString("0.0 Pa");

            // Convert the weather data into json format
            string jsonstring = JsonConvert.SerializeObject(weather);

            // send the json string to Azure IoT Hub
            await iothelper.SendDataToAzure(jsonstring);
        }
        
        private void iothelper_MessageReceivedEvent(object sender, string e)
        {
            messageTB.Text = e.ToString();
        }


    }
}

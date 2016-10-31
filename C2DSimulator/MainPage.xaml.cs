using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

namespace C2DSimulator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        const string IoTHubConnectionString = "<Your Azure IoT Hub Connection String>";
        const string deviceID = "<Your Device Name>";

        public MainPage()
        {
            this.InitializeComponent();
        }
        
        private async void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isConnected = false;

            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(IoTHubConnectionString);

            try
            {
                await registryManager.OpenAsync();
                isConnected = true;
            }
            catch
            { }

            if (!isConnected)
                return;

            ServiceClient serviceclient = ServiceClient.CreateFromConnectionString(IoTHubConnectionString);

            var serviceMessage = new Message(Encoding.ASCII.GetBytes(messageBox.Text));
            serviceMessage.Ack = DeliveryAcknowledgement.Full;
            serviceMessage.MessageId = Guid.NewGuid().ToString();

            await serviceclient.SendAsync(deviceID, serviceMessage);
        }
    }
}

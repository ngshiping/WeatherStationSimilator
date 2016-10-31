using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationSimilator
{
    public class AzureIoTHelper
    {
        public static string iotHubConnectionString = "<Your Azure IoT Hub Host Name>";
        public static string devicename = "<Your Device Name>";
        public static string deviceKey = "<Your device key>";

        public event EventHandler<string> MessageReceivedEvent;
        public static DeviceClient deviceClient = DeviceClient.Create(iotHubConnectionString, new DeviceAuthenticationWithRegistrySymmetricKey(devicename, deviceKey));
        
        public async Task SendDataToAzure(string message)
        {
            try
            {
                var msg = new Message(Encoding.UTF8.GetBytes(message));
                await deviceClient.SendEventAsync(msg);
            }
            catch
            { }
        }

        public async Task ReceiveDataFromAzure()
        {
            try
            {
                Message receivedMessage;
                string messageData;

                while (true)
                {
                    receivedMessage = await deviceClient.ReceiveAsync();
                    if (receivedMessage != null)
                    {
                        messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                        this.OnMessageReceivedEvent(messageData);
                        await deviceClient.CompleteAsync(receivedMessage);
                    }
                }
            }
            catch
            { }
        }

        protected virtual void OnMessageReceivedEvent(string s)
        {
            EventHandler<string> handler = MessageReceivedEvent;
            if (handler != null)
            {
                handler(this, s);
            }
        }


    }

}

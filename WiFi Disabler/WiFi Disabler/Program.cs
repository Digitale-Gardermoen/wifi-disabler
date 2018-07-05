using System;
using System.Net.NetworkInformation;

namespace WiFi_Disabler
{
    public static class NetworkingExample
    {
        public static void Main()
        {
            NetworkChange.NetworkAddressChanged += new
            NetworkAddressChangedEventHandler(AddressChangedCallback);
            Console.WriteLine("Listening for address changes. Press any key to exit.");
            Console.ReadLine();
        }
        static void AddressChangedCallback(object sender, EventArgs e)
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                if (n.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                Console.WriteLine(n.Name + "  State: " + n.OperationalStatus);
                if (n.NetworkInterfaceType == NetworkInterfaceType.Ethernet && n.OperationalStatus == OperationalStatus.Up)
                {
                    Console.WriteLine("Disabling WiFi...");
                }
            }
        }
    }
}

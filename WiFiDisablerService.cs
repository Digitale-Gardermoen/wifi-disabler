using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WiFi_Disabler
{

    public partial class WiFiDisablerService : ServiceBase
    {
        const long NETWORK_DISCONNECT = 27;
        const long NETWORK_CONNECT = 32;

        EventLog systemLog;
        EntryWrittenEventHandler handler;

        public WiFiDisablerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            systemLog = new EventLog("System", ".", "e1iexpress");
            systemLog.EnableRaisingEvents = true;
            handler = new EntryWrittenEventHandler(NetworkEventListener);

            systemLog.EntryWritten += handler;
        }

        private void NetworkEventListener(object sender, EntryWrittenEventArgs e)
        {
            long eventId = (e.Entry.InstanceId & 0xFFFF);
            switch (eventId)
            {
                case NETWORK_DISCONNECT:
                    handleNetworkDisconnection(e);
                    break;

                case NETWORK_CONNECT:
                    handleNetworkConnection(e);
                    break;

                default:

                    break;
            }
        }

        protected override void OnStop()
        {
            systemLog.EntryWritten -= handler;
            systemLog.Dispose();
            systemLog = null;  
        }

        public void handleNetworkDisconnection(EntryWrittenEventArgs e)
        {
            // Check other interfaces for status and enable wifi if "all" devices are disconnected
        }

        public void handleNetworkConnection(EntryWrittenEventArgs e)
        {
            // Disable WiFi if it is enabled.
        }
    }
}

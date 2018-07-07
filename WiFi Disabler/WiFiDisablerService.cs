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
        const int NETWORK_DISCONNECT = 27;
        const int NETWORK_CONNECT = 32;

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
            Int32 eventId = (Int32)(e.Entry.InstanceId & 0xFFFF);
            switch (eventId)
            {
                case NETWORK_DISCONNECT:

                    break;
                case NETWORK_CONNECT:

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
    }
}

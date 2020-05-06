using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;

namespace Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            IDictionary props = new Hashtable();
            props["port"] = 0; // let the system choose a free port
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
            TcpChannel chan = new TcpChannel(props, clientProvider, serverProvider); // instantiate the channel
            ChannelServices.RegisterChannel(chan, false); // register the channel

            ChannelDataStore data = (ChannelDataStore) chan.ChannelData;
            int port = new Uri(data.ChannelUris[0]).Port; // get the port
                
          
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteClient), "RemoteClient",
                WellKnownObjectMode.Singleton); // register my remote object for service

            ClientApp.Init("tcp://localhost:" + port + "/RemoteClient");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginWindow());
        }
    }
}
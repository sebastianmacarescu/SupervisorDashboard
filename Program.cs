using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;

namespace SupervisorDashboard
{
    static class Program
    {
        public static bool DEBUG = true;

        public static string eventHubConnectionString;
        public static string AzureAdoSqlConnectionString;
        public static string storageConnectionString;
        public static string eventHubName;
        public static DateTime jobStartTime;

        public static EventProcessorHost eventProcessorHost;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            storageConnectionString = ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString();
            eventHubConnectionString = ConfigurationManager.ConnectionStrings["AzureEventHubConnectionString"].ToString();
            AzureAdoSqlConnectionString =  ConfigurationManager.ConnectionStrings["AzureAdoSqlConnectionString"].ToString();
            eventHubName = ConfigurationManager.AppSettings["AzureEventHubName"].ToString();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void listenToEventHub()
        {
            stopListeningEventhub();
            Console.WriteLine("Connecting to eventhub");
            jobStartTime = DateTime.UtcNow;
            string eventProcessorHostName = Guid.NewGuid().ToString();
            eventProcessorHost = new EventProcessorHost(
                eventProcessorHostName,
                eventHubName,
                EventHubConsumerGroup.DefaultGroupName,
                eventHubConnectionString,
                storageConnectionString);

            var epo = new EventProcessorOptions
            {
                MaxBatchSize = 100,
                PrefetchCount = 10,
                ReceiveTimeOut = TimeSpan.FromSeconds(20),
            };

            epo.ExceptionReceived += OnExceptionReceived;

            Console.WriteLine("Registering EventProcessor...");
            eventProcessorHost.RegisterEventProcessorAsync<EventHubProcessor>(epo).Wait();
        }

        public static void stopListeningEventhub()
        {
            if (eventProcessorHost != null)
            {
                Console.WriteLine("Stop listening to eventhub");
                eventProcessorHost.UnregisterEventProcessorAsync().Wait();
                eventProcessorHost = null;
            }
        }

        public static void OnExceptionReceived(object sender, ExceptionReceivedEventArgs args)
        {
            Console.WriteLine("Event Hub exception received: {0}", args.Exception.Message);
        }
    }
}

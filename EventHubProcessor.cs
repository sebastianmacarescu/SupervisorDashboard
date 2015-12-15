using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Diagnostics;

using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SupervisorDashboard.models;

namespace SupervisorDashboard
{
    class EventHubProcessor : IEventProcessor
    {
        Stopwatch checkpointStopWatch;


        async Task IEventProcessor.CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("Processor Shutting Down. Partition '{0}', Reason: '{1}'.", context.Lease.PartitionId, reason);
            if (reason == CloseReason.Shutdown)
            {
                await context.CheckpointAsync();
            }
        }

        Task IEventProcessor.OpenAsync(PartitionContext context)
        {
            Console.WriteLine("EventHubProcessor initialized.  Partition: '{0}', Offset: '{1}'", context.Lease.PartitionId, context.Lease.Offset);
            this.checkpointStopWatch = new Stopwatch();
            this.checkpointStopWatch.Start();
            return Task.FromResult<object>(null);
        }

        async Task IEventProcessor.ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (EventData eventData in messages)
            {
                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                if (data != null && eventData.EnqueuedTimeUtc > Program.jobStartTime)
                {
                    Console.WriteLine(string.Format("Message received.  Partition: '{0}';  EnqueuedTimeUtc {1}", context.Lease.PartitionId, eventData.EnqueuedTimeUtc));
                    handleData(data, eventData.EnqueuedTimeUtc);
                }
                else
                {
                    Console.WriteLine(string.Format("Null Message received or EnqueuedTimeUtc ({0}) < jobStartTime ({1}).  Partition: '{2}'",
                        eventData.EnqueuedTimeUtc,
                        Program.jobStartTime,
                        context.Lease.PartitionId));
                }
            }

            //Call checkpoint every 5 minutes, so that worker can resume processing from the 5 minutes back if it restarts.
            if (this.checkpointStopWatch.Elapsed > TimeSpan.FromMinutes(5))
            {
                await context.CheckpointAsync();
                this.checkpointStopWatch.Restart();
            }
        }

        public void handleData(string data, DateTime EnqueuedTimeUtc)
        {
            // Create a message and add it to the queue.
            //var payloadStream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            //queueClient.Send(new BrokeredMessage(payloadStream, true));

            JArray messages;
            try
            {
                messages = JArray.Parse(data);
            }
            catch (JsonReaderException e)
            {
                messages = new JArray();
                try
                {
                    messages.Add(JObject.Parse(data));
                }
                catch (JsonReaderException err)
                {
                    Console.WriteLine(string.Format("Cannot parse json from data: Error {0}, Data: {1}", err.ToString(), data));
                    return;
                }
            }

            Database.SetInitializer<DatabaseContext>(null);
            foreach (var msg in messages.Children())
            {
                Sensor sensor;
                using (var db = new DatabaseContext())
                {

                    var sensorGuid = msg["SensorId"].Value<Guid>();
                    var sensorType = msg["type"].Value<string>();
                    var sensorStatus = msg["status"].Value<string>();

                    sensor = db.SensorSet.Where(b =>
                        b.id == sensorGuid
                        ).FirstOrDefault<Sensor>();

                    if (sensor == null)
                    {
                        sensor = new Sensor { id = sensorGuid, type = sensorType, status = sensorStatus };
                        db.SensorSet.Add(sensor);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbUpdateException e)
                        {
                            Console.WriteLine(string.Format("Error when saving into database: Error {0}, Object: {1}",
                                e.ToString(),
                                sensor.ToString()));
                            return;
                        }
                    }
                    Console.WriteLine(string.Format("Received payload for sensor", sensor.ToString()));
                    var payload = msg["data"] as JObject;

                    var sensorValue = new SensorValue {
                        value = payload["value"].Value<float>(),
                        jsonStringValue = payload["jsonStringValue"].ToString(),
                        sensor = sensor
                    };
                    db.SensorValueSet.Add(sensorValue);
                    if (Program.DEBUG)
                        Console.WriteLine(string.Format("Parsed object: {0}", sensorValue.ToString()));
                            
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        Console.WriteLine(string.Format("Error when saving into database: Error {0}, Object: {1}",
                            e.ToString(),
                            msg.ToString()));
                    }
                }
            }
        }
    }
}

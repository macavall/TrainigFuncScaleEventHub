using System;
using System.Collections.Generic;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace trainigfuncscale56FA
{
    public class ehtrigger
    {
        private readonly ILogger<ehtrigger> _logger;

        public ehtrigger(ILogger<ehtrigger> logger)
        {
            _logger = logger;
        }


        // ehconnstring will be found in local.settings.json and will not be shown in the GitHub
        // ehconnstring is the Connection String for the Event Hub Namespace
        [EventHubOutput("%EventHubName%", Connection = "ehconnstring")]
        [Function(nameof(ehtrigger))]
        public List<MyEvent> Run([EventHubTrigger("%EventHubName%", Connection = "ehconnstring")] EventData[] events)
        {
            List<MyEvent> myEventsList = new List<MyEvent>();

            foreach (EventData @event in events)
            {
                _logger.LogInformation("Event Body: {body}", @event.Body);
                _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);

                myEventsList.Add(new MyEvent
                {
                    Name = @event.Body.ToString()
                });
            }

            return myEventsList;
        }

        public class MyEvent
        {
            public string Name { get; set; }
        }
    }
}

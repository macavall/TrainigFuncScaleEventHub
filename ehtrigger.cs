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
            // The EventData[] events, above is the array of events that are received from the Event Hub
            List<MyEvent> myEventsList = new List<MyEvent>();

            // Loop through the events and log the body and content type
            // and add a new MyEvent object to the list myEventsList
            foreach (EventData @event in events)
            {
                _logger.LogInformation("Event Body: {body}", @event.Body);
                _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);

                myEventsList.Add(new MyEvent
                {
                    Name =  "Matt"
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

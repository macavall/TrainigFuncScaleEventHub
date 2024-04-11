using System;
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

        [Function(nameof(ehtrigger))]
        public void Run([EventHubTrigger("eventhub3", Connection = "ehconnstring")] EventData[] events)
        {
            foreach (EventData @event in events)
            {
                _logger.LogInformation("Event Body: {body}", @event.Body);
                _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
            }
        }
    }
}
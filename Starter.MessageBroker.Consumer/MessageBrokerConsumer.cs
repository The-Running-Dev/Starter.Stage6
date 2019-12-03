using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Starter.MessageBroker.Consumer
{
    public static class MessageBrokerConsumer
    {
        [FunctionName("MessageBrokerConsumer")]
        public static void Run([ServiceBusTrigger("%QueueName%", "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}

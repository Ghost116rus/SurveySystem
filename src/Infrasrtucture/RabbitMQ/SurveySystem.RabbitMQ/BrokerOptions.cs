namespace SurveySystem.RabbitMQ
{
    public class BrokerOptions
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public QueueSettings[] Consumers { get; set; }
        public QueueSettings[] Producers { get; set; }

    }

    public class QueueSettings
    {
        public string Queue { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public int PrefetchCount { get; set; }

        public IDictionary<string, object>? Arguments { get; set; } = null;
    }
}

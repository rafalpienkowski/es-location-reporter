using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StatlerWaldorfCorp.LocationReporter.Service.Models;
using RabbitMQ.Client;
using System.Text;

namespace StatlerWaldorfCorp.LocationReporter.Service.Events
{
    public class AMQPEventEmitter : IEventEmitter
    {
        private readonly ILogger _logger;
        private readonly AMQPOptions _rabbitOptions;
        private readonly ConnectionFactory _connectionFactory;

        public const string QUEUE_LOCATONRECORDED = "memberlocationrecorded";

        public AMQPEventEmitter(ILogger<AMQPEventEmitter> logger, IOptions<AMQPOptions> amqpOptions)
        {
            _logger = logger;
            _rabbitOptions = amqpOptions.Value;
            
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.UserName = _rabbitOptions.Username;
            _connectionFactory.Password = _rabbitOptions.Password;
            _connectionFactory.VirtualHost = _rabbitOptions.VirtualHost;
            _connectionFactory.HostName = _rabbitOptions.HostName;
            _connectionFactory.Uri = _rabbitOptions.Uri;

            _logger.LogInformation($"AMQP Event Emitter configured with URI {_rabbitOptions.Uri}");
        }

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_LOCATONRECORDED,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );
                    var jsonPayload = locationRecordedEvent.ToJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_LOCATONRECORDED,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}
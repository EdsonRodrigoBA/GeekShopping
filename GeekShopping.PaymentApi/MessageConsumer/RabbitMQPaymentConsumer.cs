// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using System.Text;
using System.Text.Json;

using GeekShopping.OrderApi.Messages;
using GeekShopping.PaymentApi.Messages;
using GeekShopping.PaymentApi.RabbitMQMessageSender;
using GeekShopping.PaymentProcessor;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.PaymentApi.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _chanel;
        private readonly IProcessPayment _processPayment;
        private IRabbitMQMessageSender _rabbitMQMessage;
        private const string ExchangePaymentName = "FanoutPaymentUpdateExchange";
        private  string queueName = "";

        public RabbitMQPaymentConsumer(IProcessPayment processPayment, IRabbitMQMessageSender rabbitMQMessage)
        {
            _processPayment = processPayment;
            _rabbitMQMessage = rabbitMQMessage;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();
            _chanel = _connection.CreateModel();
            //_chanel.QueueDeclare("orderPaymentProcessQueue", false, false, false, null);
            //declara o exchange
            _chanel.ExchangeDeclare(ExchangePaymentName, ExchangeType.Fanout);
            //seta o nome da fila com base no nome do exchange
            queueName = _chanel.QueueDeclare().QueueName;
            //faz o binde
            _chanel.QueueBind(queueName, ExchangePaymentName, string.Empty);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_chanel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content);
                processPayment(vo).GetAwaiter().GetResult();
                _chanel.BasicAck(evt.DeliveryTag, false);
            };

            _chanel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task processPayment(PaymentMessage vo)
        {
            //simula o processamento do pagamento
            var result = _processPayment.PaymentProcessor();
            UpdatePaymentResultMessage paymeResult = new UpdatePaymentResultMessage
            {
                Status = result,
                Email = vo.Email,
                OrderId = vo.OrderId
            };
            try
            {

                _rabbitMQMessage.SendMessage(paymeResult);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

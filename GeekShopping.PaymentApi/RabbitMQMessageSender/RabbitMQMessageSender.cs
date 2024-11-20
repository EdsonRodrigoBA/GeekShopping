// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using System.Text;
using System.Text.Json;

using GeekShopping.MessageBuss;
using GeekShopping.OrderApi.Messages;

using RabbitMQ.Client;

namespace GeekShopping.PaymentApi.RabbitMQMessageSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private  IConnection _connection;
        private const string ExchangePaymentName = "FanoutPaymentUpdateExchange"; 
        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        /// <summary>
        /// Trabalhando com exchange fanout para processar pagamento e enviar email
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var chanel = _connection.CreateModel();
                chanel.ExchangeDeclare(ExchangePaymentName, ExchangeType.Fanout, false);
                byte[] body = GetMessageAsByArray(message);
                chanel.BasicPublish(ExchangePaymentName, "", null, body);
            }

        }

        private byte[] GetMessageAsByArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    Password = _password,
                    UserName = _userName
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool ConnectionExists()
        {
            if (_connection != null) return true;

            CreateConnection();
            return _connection != null;
        }


    }
}

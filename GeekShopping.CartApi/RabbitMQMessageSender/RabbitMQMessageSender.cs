// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using System.Text;
using System.Text.Json;

using GeekShopping.CartApi.Model.Message;
using GeekShopping.MessageBuss;

using RabbitMQ.Client;

namespace GeekShopping.CartApi.RabbitMQMessageSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private  IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            if (ConnectionExists())
            {
                using var chanel = _connection.CreateModel();
                chanel.QueueDeclare(queueName, false, false, false, null);
                byte[] body = GetMessageAsByArray(message);
                chanel.BasicPublish("", queueName, null, body);
            }

        }

        private byte[] GetMessageAsByArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize<CheckoutHeaderVO>((CheckoutHeaderVO)message, options);
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

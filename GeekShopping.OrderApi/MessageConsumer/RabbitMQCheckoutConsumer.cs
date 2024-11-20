// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using System.Text;
using System.Text.Json;

using GeekShopping.OrderApi.Messages;
using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.RabbitMQMessageSender;
using GeekShopping.OrderApi.Repository;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.OrderApi.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        private IConnection _connection;
        private IModel _chanel;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(OrderRepository orderRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _orderRepository = orderRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();
            _chanel = _connection.CreateModel();
            _chanel.QueueDeclare("checkout", false, false, false, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_chanel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderVO vo = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);
                processOrder(vo).GetAwaiter().GetResult();
                _chanel.BasicAck(evt.DeliveryTag, false);
            };

            _chanel.BasicConsume("checkout", false, consumer);

            return Task.CompletedTask;
        }

        private async Task processOrder(CheckoutHeaderVO vo)
        {
            OrderHeader orderHeader = new()
            {
                CardNumber = vo.CardNumber,
                CouponCode = vo.CouponCode,
                UserId = vo.UserId,
                CVV = vo.CVV,
                DateTime = vo.DateTime,
                DiscountAmount = vo.DiscountAmount,
                Email = vo.Email,
                ExpiryMonthYear = vo.ExpiryMothYear,
                FirstName = vo.FirstName,
                LastName = vo.LastName,
                OrderDateTime = DateTime.Now,
                OrderDetails = new List<OrderDetail>(),
                PaymentStatus = false,
                Phone = vo.Phone,
                PurchaseAmount = vo.PurchaseAmount,

            };
            foreach (var cartDetail in vo.CartDetails)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    productId = cartDetail.productId,
                    price = cartDetail.product.price,
                    productName = cartDetail.product.name,
                    count = cartDetail.count,
                };
                orderHeader.OrderTotalItens = orderHeader.OrderTotalItens + cartDetail.count;
                orderHeader.OrderDetails.Add(orderDetail);
            }

            await _orderRepository.AddOrder(orderHeader);
            PaymentVO paymentVO = new PaymentVO()
            {
                 Name = $"{orderHeader.FirstName} {orderHeader.LastName}",
                 CardNumber = orderHeader.CardNumber,
                 CVV = orderHeader.CVV,
                 ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                 OrderId = orderHeader.Id,
                 PurchaseAmount = orderHeader.PurchaseAmount,
                 Email = orderHeader.Email
            };
            try
            {
                _rabbitMQMessageSender.SendMessage(paymentVO, "orderPaymentProcessQueue");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Newtonsoft.Json;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PizzeriaDOM.Pages
{
    /// <summary>
    /// Logique d'interaction pour Messages.xaml
    /// </summary>
    public partial class Messages : UserControl
    {
        static Dictionary<int, string> KitchenMessage = new Dictionary<int, string>();
        static List<String> DeliveryMessage = new List<String>();
        static List<Order> WaitingDelivery = new List<Order>();

        private DispatcherTimer kitchenTimer;
        public Messages()
        {
            InitializeComponent();
        }

        public void LoadMessages()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            Security(connection);
            Customer(connection);
            Kitchen(connection);
            Clerk(connection);
        }

        private void Security(IConnection connection)
        {
            using (var channelSecurity = connection.CreateModel())
            {
                var consumerSecurity = new EventingBasicConsumer(channelSecurity);
                consumerSecurity.Received += (model, ea) =>
                {
                };

                channelSecurity.BasicConsume(queue: "security",
                                     autoAck: true,
                                     consumer: consumerSecurity);
            }
        }

        private void Customer(IConnection connection)
        {
            using (var channelCustomer = connection.CreateModel())
            {
                var consumerCustomer = new EventingBasicConsumer(channelCustomer);
                consumerCustomer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Order order = JsonConvert.DeserializeObject<Order>(message);
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        string msg = $"Hello {order.Customer.Surname + " " + order.Customer.FirstName} your order No : {order.ID} is now in preparation !\n";
                        CustomerConsole.Text += msg;

                    }));
                };
                channelCustomer.BasicConsume(queue: "customer",
                                     autoAck: true,
                                     consumer: consumerCustomer);
            }
        }

        private void Kitchen(IConnection connection)
        {
            using (var channelKitchen = connection.CreateModel())
            {
                var consumerKitchen = new EventingBasicConsumer(channelKitchen);
                consumerKitchen.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Order order = JsonConvert.DeserializeObject<Order>(message);

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        string msg = $"{order.ID} Time remaining: {order.KitchenCountdown} seconds";
                        KitchenMessage.Add(order.ID, msg);
                        // Création d'un timer et définission intervalle = 1s, éxecuter la fonction
                        kitchenTimer = new DispatcherTimer();
                        kitchenTimer.Interval = TimeSpan.FromSeconds(1);
                        kitchenTimer.Tick += (sender, e) =>
                        {
                            msg = $"{order.ID} Time remaining: {order.KitchenCountdown} seconds";
                            // Mettez à jour le message de la console Kitchen
                            KitchenMessage[order.ID] = msg;
                            DisplayKitchenMessage();

                            // Réduisez le temps restant
                            order.KitchenCountdown--;

                            // Si le compte à rebours atteint 0, arrêtez le timer
                            if (order.KitchenCountdown < 0)
                            {
                                kitchenTimer.Stop();
                                KitchenMessage.Remove(order.ID);
                                DisplayKitchenMessage();
                                sendDelivery(order);
                            }
                        };

                        kitchenTimer.Start();
                    }));
                };

                channelKitchen.BasicConsume(queue: "kitchen", autoAck: true, consumer: consumerKitchen);

                DisplayKitchenMessage();
            }
        }

        private void DisplayKitchenMessage()
        {
            KitchenConsole.Text = string.Empty;
            foreach (var keyValue in KitchenMessage)
            {
                KitchenConsole.Text += keyValue.Value + "\n";
            }
        }

        private void sendDelivery(Order order)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare("Direct", type: ExchangeType.Direct);

            string serializedObject = JsonConvert.SerializeObject(order);
            var body = Encoding.UTF8.GetBytes(serializedObject);

            channel.QueueBind(queue: "delivery",
                  exchange: "Direct",
                  routingKey: "delivery"
                  );

            channel.BasicPublish(exchange: "Direct",
                                 routingKey: "delivery",
                                 basicProperties: null,
                                 body: body);

            Delivery(connection);
        }

        private void Clerk(IConnection connection)
        {
            using (var channelClerk = connection.CreateModel())
            {

                Trace.WriteLine("Clerk func");

                var consumerClerk = new EventingBasicConsumer(channelClerk);
                consumerClerk.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Order order = JsonConvert.DeserializeObject<Order>(message);
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ClerkConsole.Text += order.ToString();
                    }));

                };

                channelClerk.BasicConsume(queue: "clerk",
                                     autoAck: true,
                                     consumer: consumerClerk);
            }

        }

        private void Delivery(IConnection connection)
        {
            using (var channelDelivery = connection.CreateModel())
            {
                var consumerDelivery = new EventingBasicConsumer(channelDelivery);
                consumerDelivery.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Order order = JsonConvert.DeserializeObject<Order>(message);

                    Dispatcher.BeginInvoke(new Action(() =>
                    {           
                        string msg = "Order No " + order.ID + " received, waiting for delivery";
                        DeliveryConsole.Text += msg;
                        WaitingDelivery.Add(order);
                        Task<string> deliveryResult = TryDeliver(order); // Attendre le résultat de TryDeliver
                        deliveryResult.ContinueWith(taskResult =>
                        {
                            string result = taskResult.Result;
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                DeliveryConsole.Text += result +"\n";
                            } ));
                        });
                    }));
                };

                channelDelivery.BasicConsume(queue: "delivery",
                                     autoAck: true,
                                     consumer: consumerDelivery);
            }

        }

        private async Task<string> TryDeliver(Order order)
        {
            Trace.WriteLine("Je suis order numéro : " + order.ID);
            string orderStatus = "Order No " + order.ID + " could not be delivered in time. Abandonned";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += async (sender, e) =>
            {
                Trace.WriteLine("Check d'un delivery dispo pour order " + order.ID);
                DeliveryMan deliveryMan = IOFile.GetDeliveryMan();
                if (deliveryMan != null)
                {
                    Trace.WriteLine("Delivery trouvé");
                    await Deliver(order, deliveryMan);
                    orderStatus = "Order No " + order.ID + " delivered";
                    Trace.WriteLine("Statut actualisé");

                    timer.Stop();
                }
            };

            timer.Start();

            await Task.Delay(10000);
            if (WaitingDelivery.Contains(order))
            {
                WaitingDelivery.Remove(order);
                timer.Stop();

            }
            return orderStatus;

        }

        private async Task Deliver(Order order, DeliveryMan deliveryMan) 
        {
            IOFile.updateDeliveryManDisponibility(deliveryMan, false);
            WaitingDelivery.Remove(order);
            await Task.Delay(5000);
            Trace.WriteLine("Fin livraison");
            IOFile.updateDeliveryManDisponibility(deliveryMan, true);
        }

    }
    }


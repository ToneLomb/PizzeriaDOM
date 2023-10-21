using Newtonsoft.Json;
using PizzeriaDOM.src.classes;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzeriaDOM.Pages
{
    /// <summary>
    /// Logique d'interaction pour Messages.xaml
    /// </summary>
    public partial class Messages : UserControl
    {
        static List<String> CustomerMessage = new List<String>();
        public Messages()
        {
            InitializeComponent();


            

            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            Security(connection);
            Customer(connection);
            Kitchen(connection);
            Clerk(connection);
            Delivery(connection);
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
                        string msg = "La commande" + order.ID + "a bien été prise en compte";
                        CustomerMessage.Add(msg);
                        

                    }));
                    
                };

                channelCustomer.BasicConsume(queue: "customer",
                                     autoAck: true,
                                     consumer: consumerCustomer);

                foreach (string message in CustomerMessage)
                {
                    CustomerConsole.Text += message + "\n";
                }

            }

        }

        private void Kitchen(IConnection connection)
        {
            using (var channelKitchen = connection.CreateModel())
            {

                Trace.WriteLine("Kitchen func");

                var consumerKitchen = new EventingBasicConsumer(channelKitchen);
                consumerKitchen.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Order order = JsonConvert.DeserializeObject<Order>(message);
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        KitchenConsole.Text += order.ToString();
                    }));

                };

                channelKitchen.BasicConsume(queue: "kitchen",
                                     autoAck: true,
                                     consumer: consumerKitchen);
            }

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

                Trace.WriteLine("Delivery func");

                var consumerDelivery = new EventingBasicConsumer(channelDelivery);
                consumerDelivery.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Order order = JsonConvert.DeserializeObject<Order>(message);
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        DeliveryConsole.Text += order.ToString();
                    }));

                };

                channelDelivery.BasicConsume(queue: "delivery",
                                     autoAck: true,
                                     consumer: consumerDelivery);
            }
        }
    }
}

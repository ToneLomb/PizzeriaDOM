using Newtonsoft.Json;
using PizzeriaDOM.src.classes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour Kitchen.xaml
    /// </summary>
    public partial class Kitchen : UserControl
    {

        public Kitchen()
        {
            InitializeComponent();

           /* var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            channel.QueueBind(queue: "kitchen",
                              exchange: "toKitchen",
                              routingKey: "kitchen");

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Order order = JsonConvert.DeserializeObject<Order>(message);
                //MessageBox.Text = order.ToString();
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    MessageBox.Text += order.ToString();
                    Trace.WriteLine(order.ToString());

                }));

                

            };

            channel.BasicConsume(queue: "kitchen",
                                 autoAck: true,
                                 consumer: consumer);
           */
        }
    }
}

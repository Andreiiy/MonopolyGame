using ClientMonopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Monopoly_WPF
{
    /// <summary>
    /// Interaction logic for WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        Client client;
        string HOST = "http://localhost:63569/api/";

        public WindowRegistration()
        {
            InitializeComponent();
            client = new Client(HOST);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = tbUserName.Text;
            string password = tbPassword.Text;
            string email = tbEmail.Text;
            if (userName == "" || email == "" || password == "")
                lRegistr.Content = "Fill in all form fields";
            else
            {
                try
                {
                    if (Male.IsChecked == true)
                        client.Registration(userName, email, password,"male");
                    else
                    client.Registration(userName, email, password,"female");
                    this.DialogResult = true;
                }
                catch
                {
                lRegistr.Content = "No server connection";
                }
                
                }
           
           
           
        }
    }
}

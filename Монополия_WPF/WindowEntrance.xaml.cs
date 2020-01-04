using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using ClientMonopoly;

namespace Monopoly_WPF
{
    /// <summary>
    /// Interaction logic for WindowEntrance.xaml
    /// </summary>
    public partial class WindowEntrance : Window
    {
        Client client;
         string HOST = "http://localhost:63569/api/";
       /// string HOST = "http://192.168.2.101:63569/api/";
        int userID;
       
        public WindowEntrance()
        {
            InitializeComponent();
            //MainWindow windowGame = new MainWindow();
            //windowGame.Show();
            client = new Client(HOST);
            tbUserName.Focus();
            
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistration wRegistr = new WindowRegistration();

            if (wRegistr.ShowDialog() == true)
                lEntry.Content = "Registration completed successfuly";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUserName.Text;
            string password = tbPassword.Password.ToString();
            CheckEntranceAsync(username, password);
            lEntry.Content = "Please wait check your password";


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Button_Click(null, null);
            
        }


        private async void CheckEntranceAsync(string username, string password)
        {
            await Task.Run(() => CheckEntrance(username, password));
        }

        private void CheckEntrance( string username, string password)
        {
            Dispatcher.Invoke(DispatcherPriority.ContextIdle, new Action(delegate ()
             {
                 
                 if (username == "" || password == "")
                     lEntry.Content = "Fill in all form fields";
                 else
                 {
                     try
                     {
                         string entry = client.Entry(tbUserName.Text, tbPassword.Password.ToString());

                         if (entry != "false")
                         {
                             userID = Int32.Parse(entry);
                             wSystemGame wSystemGame = new wSystemGame(userID);
                             wSystemGame.Show();
                             this.Close();
                         }
                         else
                         {
                             lEntry.Content = "Username or password is not correct";
                             tbUserName.Text = "";
                             tbPassword.Password = "";
                         }
                     }
                     catch
                     {
                         lEntry.Content = "No server connection";
                     }
                 }
             }));

            
        }
    }
}


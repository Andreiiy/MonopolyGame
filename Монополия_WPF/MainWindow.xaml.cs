using ClientMonopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Monopoly_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        GameMonopoly game;
        MyGameMonopoly mygame;
        Client client;
        GameInfo gameInfo;
        int userID;
        int playerID;
        bool chekMyGame = false;
        bool difficult;
        Monopoly_WPF.ServiceChatWCFMonopoly.ServiceChatClient clientWCF;
       
        public MainWindow(Client client, Monopoly_WPF.ServiceChatWCFMonopoly.ServiceChatClient clientWCF,GameInfo gameInfo,int userID, bool difficult)
        {
            InitializeComponent();
          
            this.client = client;
            this.clientWCF = clientWCF;
            this.gameInfo = gameInfo;
            this.userID = userID;
            this.difficult = difficult;
            lGameOver.Height = 0;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (gameInfo != null)
            {
                
                game = new GameMonopoly(this, this.canvas, client, gameInfo, userID);
                var plaerInGame = client.GetPlayer(userID, gameInfo.ID_Game);
                playerID = plaerInGame.ID;
                clientWCF.ConnectPlayers(userID, gameInfo.ID_Game);
               
            }
              
            else
            {
                chekMyGame = true;
              mygame = new MyGameMonopoly(this, this.canvas, client, userID, difficult);
              
            }
               
        }
        /// <summary>
        /// cube throw handler button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            Imagechance.Source = new BitmapImage(new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\Chance.png", UriKind.Relative));
            if (mygame == null) 
            {
                game.MoveMyPlayer();
            }
            else
            {
               mygame.MoveMyPlayer();
            }
        }

        private void BtnChat_Click(object sender, RoutedEventArgs e)
        {
           
            if (lbChat.Width==0)
            {
                lbChat.Width = 405;
                tbSendMsg.Width = 405;
                Canvas.SetLeft(btnChat, 814);
            }
            else
            {
                Canvas.SetLeft(btnChat, 1217);
                lbChat.Width = 0;
                tbSendMsg.Width = 0;
            }
        }

       

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

       
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            if (e.Key == System.Windows.Input.Key.Enter && tbSendMsg.IsKeyboardFocused == true)
            {
                clientWCF.SendMsgtoPlayers(tbSendMsg.Text.ToString(), userID, gameInfo.ID_Game);
                lbChat.Items.Add(tbSendMsg.Text.ToString());
                lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
                tbSendMsg.Text = "Send Message";
            }
            e.Handled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!chekMyGame)
            {
                client.PlayerLost(playerID);
                clientWCF.DisconnectUserFromGameAsync(userID);
            }
           
        }
    }
}

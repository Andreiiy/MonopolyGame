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
    /// Interaction logic for WindowAddOrDellUserinGame.xaml
    /// </summary>
    public partial class WindowAddOrDellUserinGame : Window
    {
        
        public bool chek;
        public WindowAddOrDellUserinGame(string question)
        {
            InitializeComponent();
            label.Content = question;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chek = true;
            this.DialogResult = true;
        }

        private void BtnNou_Click(object sender, RoutedEventArgs e)
        {
            chek = false;
            this.DialogResult = true;
        }
    }
}

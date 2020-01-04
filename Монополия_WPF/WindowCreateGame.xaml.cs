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
    /// Interaction logic for WindowCreateGame.xaml
    /// </summary>
    public partial class WindowCreateGame : Window
    {
        public bool playwithFrends;
        public bool playwithAll;

        public WindowCreateGame()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (playinFrend.IsChecked == true)
                playwithFrends = true;
            if(playAll.IsChecked == true)
                playwithAll = true;
            this.DialogResult = true;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

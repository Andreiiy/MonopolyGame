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
   

    public partial class WindowSellField : Window
    {

        List<Field> fields;
       public Field fieldSell;

        public WindowSellField(List<Field> fields)
        {
            InitializeComponent();
            this.fields = fields;
            lbFields.DataContext = this.fields;

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            int selected = int.Parse(lbFields.SelectedIndex.ToString());
            fieldSell = fields[selected];

            this.DialogResult = true;
        }
    }
}

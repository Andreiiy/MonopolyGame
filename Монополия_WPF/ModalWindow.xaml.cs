
using System.Windows;


namespace Monopoly_WPF
{
    
    public partial class ModalWindow : Window
    {
      public  bool bay = false;
      private  Field field;
        public ModalWindow(Field field)
        {
            InitializeComponent();
            this.field = field;
            lNameFild.Content = field.name;
            lprice.Content = field.price;
        }

        private void BtnBay_Click(object sender, RoutedEventArgs e)
        {
            bay = true;
            this.DialogResult = true;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

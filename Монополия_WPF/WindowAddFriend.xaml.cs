using System.Windows;
using System.Windows.Media.Imaging;


namespace Monopoly_WPF
{
    /// <summary>
    /// Interaction logic for WindowAddFriend.xaml
    /// </summary>
    public partial class WindowAddFriend : Window
    {
        public bool chek;
        public WindowAddFriend(User user)
        {
            InitializeComponent();
            Images images = new Images();
            label.Content = "Do you want to add " + user.Name + " to your friends?";
            imPlayer.Source = new BitmapImage(images.GetRandomImage(user.Gender));
        }

        //Button Yes
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chek = true;
            this.DialogResult = true;
        }
        //Button No
        private void BtnNou_Click(object sender, RoutedEventArgs e)
        {
            chek = false;
            this.DialogResult = true;
        }
    }
}

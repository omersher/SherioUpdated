using Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace AppDesignXAML.pages
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            PasswordErrorText.Visibility = Visibility.Collapsed;
            PasswordBoxInput.BorderBrush = System.Windows.Media.Brushes.LightGray;

            if (string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBoxInput.Password))
            {
                ShowError("נא למלא אימייל וסיסמה");
                return;
            }

            UserDB db = new UserDB();
            UserList users = db.SelectAll();

            var user = users.FirstOrDefault(u =>
                u.Email == EmailBox.Text &&
                u.PassHash == PasswordBoxInput.Password);

            if (user == null)
            {
                ShowError("אימייל או סיסמה שגויים");
                return;
            }

            NavigationService.Navigate(new Home());
        }

        private void ShowError(string message)
        {
            PasswordErrorText.Text = message;
            PasswordErrorText.Visibility = Visibility.Visible;
            PasswordBoxInput.BorderBrush = System.Windows.Media.Brushes.Red;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }

        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

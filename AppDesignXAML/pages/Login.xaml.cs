using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model;
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
            string email = EmailBox.Text;
            string password = PasswordBoxInput.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("נא למלא אימייל וסיסמה");
                return;
            }

            UserDB db = new UserDB();
            UserList users = db.SelectAll();

            User user = users
                .FirstOrDefault(u => u.Email == email && u.PassHash == password);

            if (user == null)
            {
                MessageBox.Show("אימייל או סיסמה שגויים");
                return;
            }

            MessageBox.Show($"ברוך הבא {user.FullName}");

            NavigationService.Navigate(new Home());
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }
    }
}

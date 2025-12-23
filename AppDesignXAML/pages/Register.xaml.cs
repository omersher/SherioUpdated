using System.Windows;
using System.Windows.Controls;
using Model;
using ViewModel;

namespace AppDesignXAML.pages
{
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            // בדיקות בסיס
            if (string.IsNullOrWhiteSpace(FullNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("נא למלא את כל השדות החיוניים");
                return;
            }

            User user = new User
            {
                FullName = FullNameBox.Text,
                GuestID = GuestIdBox.Text,
                Email = EmailBox.Text,
                Phone = PhoneBox.Text,
                PassHash = PasswordBox.Password
            };

            UserDB db = new UserDB();
            db.Insert(user);

            MessageBox.Show("נרשמת בהצלחה!");

            // חזרה ללוגין
            NavigationService.Navigate(new Login());
        }
    }
}

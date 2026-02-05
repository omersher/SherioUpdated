using Model;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace AppDesignXAML.pages
{
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User newUser = new User
                {
                    FullName = FullNameBox.Text,
                    GuestID = GuestIdBox.Text,   // ← תיקון
                    Email = EmailBox.Text,
                    Phone = PhoneBox.Text,
                    PassHash = PasswordBox.Password
                };

                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5064/")
                };

                HttpResponseMessage response =
                    await client.PostAsJsonAsync("api/Users/Insert", newUser);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("שגיאה בהרשמה");
                    return;
                }

                MessageBox.Show("נרשמת בהצלחה!");
                NavigationService.Navigate(new Login());
            }
            catch
            {
                MessageBox.Show("שגיאה בהתחברות לשרת");
            }
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());

        }

    }
}

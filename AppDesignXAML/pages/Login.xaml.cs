using Model;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

namespace AppDesignXAML.pages
{
    public partial class Login : Page
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new System.Uri("http://localhost:5064/")
        };

        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            PasswordErrorText.Visibility = Visibility.Collapsed;

            try
            {
                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5064/");

                string json =
                    $"{{ \"email\": \"{EmailBox.Text.Trim()}\", \"passHash\": \"{PasswordBoxInput.Password.Trim()}\" }}";

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync("api/Users/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    ((MainWindow)Application.Current.MainWindow)
                        .MainFrame.Navigate(new Home());
                    return;
                }

                PasswordErrorText.Text = "אימייל או סיסמה שגויים";
                PasswordErrorText.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                PasswordErrorText.Text = "שגיאה בהתחברות לשרת";
                PasswordErrorText.Visibility = Visibility.Visible;
            }
        }


        private void ShowError(string message)
        {
            PasswordErrorText.Text = message;
            PasswordErrorText.Visibility = Visibility.Visible;
        }

        private void ClearErrors()
        {
            PasswordErrorText.Text = "";
            PasswordErrorText.Visibility = Visibility.Collapsed;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());

        }

    }
}

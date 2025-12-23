using System;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SherioAppDesign
{
    public partial class Login : Page
    {
        private readonly OleDbConnection con;
        private readonly OleDbCommand cmd;
        private OleDbDataReader dr;

        public Login()
        {
            InitializeComponent();

            // Build Access connection
            string dbPath = System.IO.Path.Combine(Environment.CurrentDirectory, "sherio.accdb");

            con = new OleDbConnection(
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;");

            cmd = new OleDbCommand();
            cmd.Connection = con;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                string user = txtUsername.Text.Trim();
                string pass = txtPassword.Password.Trim();

                if (VerifyUser(user, pass))
                {
                    MessageBox.Show("התחברת בהצלחה",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Example navigation:
                    // NavigationService.Navigate(new HomePage());
                }
                else
                {
                    MessageBox.Show("שם משתמש או סיסמה שגויים",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool VerifyUser(string username, string password)
        {
            con.Open();

            cmd.Parameters.Clear();
            cmd.CommandText =
                "SELECT Status FROM Users WHERE Username = ? AND Password = ?";

            cmd.Parameters.AddWithValue("?", username);
            cmd.Parameters.AddWithValue("?", password);

            dr = cmd.ExecuteReader();

            bool success = false;

            if (dr.Read())
            {
                success = Convert.ToBoolean(dr["Status"]);
            }

            dr.Close();
            con.Close();

            return success;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Allows dragging the window from inside a Page
            Window win = Window.GetWindow(this);
            if (win != null && e.LeftButton == MouseButtonState.Pressed)
                win.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model; // ודא שאתה משתמש ב-namespace הנכון עבור City ו-CityDB
using ViewModel; // ודא שאתה משתמש ב-namespace הנכון

namespace AppDesignXAML.pages
{
    public partial class Home : Page
    {
        // הערכים ההתחלתיים נטענים אוטומטית בהפעלה
        int adults = 2, kids = 0, rooms = 1;

        public Home()
        {
            InitializeComponent();
            // ודא שהאובייקט CityDB זמין
            CitiesList.ItemsSource = new CityDB().SelectAll();
            UpdateGuestsText(); // קוראים לזה כדי להציג את הערכים ההתחלתיים ב-GuestsText
        }

        // ===== Cities =====
        private void Destination_Click(object sender, MouseButtonEventArgs e)
        {
            CitiesPopup.IsOpen = true;
        }

        private void CitiesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CitiesList.SelectedItem is City city)
            {
                SelectedCityText.Text = city.CityName;
                CitiesPopup.IsOpen = false;
            }
        }

        // ===== Dates =====
        private void Dates_Click(object sender, MouseButtonEventArgs e)
        {
            DatesPopup.IsOpen = true;
        }

        private void DatesConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (CalendarRange.SelectedDates.Count > 0)
            {
                var start = CalendarRange.SelectedDates[0];
                // שימוש באופרטור ה-Index החדש (^)
                var end = CalendarRange.SelectedDates[^1];
                DatesText.Text = $"{start:dd/MM} – {end:dd/MM}";
                DatesPopup.IsOpen = false;
            }
        }

        // ===== Guests =====
        private void Guests_Click(object sender, MouseButtonEventArgs e)
        {
            GuestsPopup.IsOpen = true;
        }

        private void UpdateGuestsText()
        {
            // עדכון הטקסט הראשי
            GuestsText.Text = $"{adults + kids} נוסעים · חדר {rooms}";
            // עדכון הטקסטים בתוך ה-Popup
            AdultsText.Text = adults.ToString();
            KidsText.Text = kids.ToString();
            RoomsText.Text = rooms.ToString();
        }

        private void AdultsPlus_Click(object sender, RoutedEventArgs e) { adults++; UpdateGuestsText(); }
        private void AdultsMinus_Click(object sender, RoutedEventArgs e) { if (adults > 1) adults--; UpdateGuestsText(); }

        private void KidsPlus_Click(object sender, RoutedEventArgs e) { kids++; UpdateGuestsText(); }
        private void KidsMinus_Click(object sender, RoutedEventArgs e) { if (kids > 0) kids--; UpdateGuestsText(); }

        private void RoomsPlus_Click(object sender, RoutedEventArgs e) { rooms++; UpdateGuestsText(); }
        private void RoomsMinus_Click(object sender, RoutedEventArgs e) { if (rooms > 1) rooms--; UpdateGuestsText(); }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // לוגיקת חיפוש
        }

        private void GuestsConfirm_Click(object sender, RoutedEventArgs e)
        {
            GuestsPopup.IsOpen = false;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // ודא שהמחלקה Login קיימת ב-namespace AppDesignXAML.pages
            NavigationService.Navigate(new Login());
        }
    }
}
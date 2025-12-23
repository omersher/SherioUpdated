    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Model;

    namespace ApiInterface
    {
        public class ApiService : IApi
        {
        private readonly HttpClient client;
        private readonly string uri;

        // בנאי ברירת מחדל
        public ApiService()
        {
            uri = "https://tbr6pfqx-5064.euw.devtunnels.ms";
            client = new HttpClient();
        }

        public ApiService(HttpClient client, string baseUri)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.uri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        // ---------- Helpers ----------
        private static async Task<T> ReadAsAsync<T>(HttpResponseMessage resp)
            {
                resp.EnsureSuccessStatusCode();
                var data = await resp.Content.ReadFromJsonAsync<T>();
                if (data == null)
                    throw new InvalidOperationException("Empty response body.");
                return data;
            }

            private async Task<T> GetAsync<T>(string path)
                => await client.GetFromJsonAsync<T>($"{uri}/{path}")
                   ?? throw new InvalidOperationException("Empty response body.");

            private async Task<int> PostAsync<TBody>(string path, TBody body)
            {
                var resp = await client.PostAsJsonAsync($"{uri}/{path}", body);
                return await ReadAsAsync<int>(resp);
            }

            private async Task<int> PutAsync<TBody>(string path, TBody body)
            {
                var resp = await client.PutAsJsonAsync($"{uri}/{path}", body);
                return await ReadAsAsync<int>(resp);
            }

            private async Task<int> DeleteAsync(string path)
            {
                var resp = await client.DeleteAsync($"{uri}/{path}");
                return await ReadAsAsync<int>(resp);
            }

            // ===================== Cities =====================
            public Task<CityList> GetAllCitiesAsync()
                => GetAsync<CityList>("api/City/GetAll");

            public Task<City?> GetCityByIdAsync(int id)
                => GetAsync<City?>($"api/City/GetById/{id}");

            public Task<int> InsertCityAsync(City c)
                => PostAsync("api/City/Insert", c);

            public Task<int> UpdateCityAsync(City c)
                => PutAsync("api/City/Update", c);

            public Task<int> DeleteCityAsync(int id)
                => DeleteAsync($"api/City/Delete/{id}");

            // ===================== Users =====================
            public Task<UserList> GetAllUsersAsync()
                => GetAsync<UserList>("api/Users/GetAll");

            public Task<User?> GetUserByIdAsync(int id)
                => GetAsync<User?>($"api/Users/GetById/{id}");

            public Task<int> InsertUserAsync(User u)
                => PostAsync("api/Users/Insert", u);

            public Task<int> UpdateUserAsync(User u)
                => PutAsync("api/Users/Update", u);

            public Task<int> DeleteUserAsync(int id)
                => DeleteAsync($"api/Users/Delete/{id}");

            // ===================== Owners =====================
            public Task<OwnerList> GetAllOwnersAsync()
                => GetAsync<OwnerList>("api/Owners/GetAll");

            public Task<Owner?> GetOwnerByIdAsync(int id)
                => GetAsync<Owner?>($"api/Owners/GetById/{id}");

            public Task<int> InsertOwnerAsync(Owner o)
                => PostAsync("api/Owners/Insert", o);

            public Task<int> UpdateOwnerAsync(Owner o)
                => PutAsync("api/Owners/Update", o);

            public Task<int> DeleteOwnerAsync(int id)
                => DeleteAsync($"api/Owners/Delete/{id}");

            // ===================== Hotels =====================
            public Task<HotelList> GetAllHotelsAsync()
                => GetAsync<HotelList>("api/Hotels/GetAll");

            public Task<Hotel?> GetHotelByIdAsync(int id)
                => GetAsync<Hotel?>($"api/Hotels/GetById/{id}");

            public Task<int> InsertHotelAsync(Hotel h)
                => PostAsync("api/Hotels/Insert", h);

            public Task<int> UpdateHotelAsync(Hotel h)
                => PutAsync("api/Hotels/Update", h);

            public Task<int> DeleteHotelAsync(int id)
                => DeleteAsync($"api/Hotels/Delete/{id}");

            // ===================== Rooms =====================
            public Task<RoomList> GetAllRoomsAsync()
                => GetAsync<RoomList>("api/Rooms/GetAll");

            public Task<Room?> GetRoomByIdAsync(int id)
                => GetAsync<Room?>($"api/Rooms/GetById/{id}");

            public Task<int> InsertRoomAsync(Room r)
                => PostAsync("api/Rooms/Insert", r);

            public Task<int> UpdateRoomAsync(Room r)
                => PutAsync("api/Rooms/Update", r);

            public Task<int> DeleteRoomAsync(int id)
                => DeleteAsync($"api/Rooms/Delete/{id}");

            // ===================== RoomImages =====================
            public Task<RoomImagesList> GetAllRoomImagesAsync()
                => GetAsync<RoomImagesList>("api/RoomImages/GetAll");

            public Task<RoomImage?> GetRoomImageByIdAsync(int id)
                => GetAsync<RoomImage?>($"api/RoomImages/GetById/{id}");

            public Task<int> InsertRoomImageAsync(RoomImage ri)
                => PostAsync("api/RoomImages/Insert", ri);

            public Task<int> UpdateRoomImageAsync(RoomImage ri)
                => PutAsync("api/RoomImages/Update", ri);

            public Task<int> DeleteRoomImageAsync(int id)
                => DeleteAsync($"api/RoomImages/Delete/{id}");

            // ===================== RoomAvailability =====================
            public Task<RoomAvailabilityList> GetAllRoomAvailabilityAsync()
                => GetAsync<RoomAvailabilityList>("api/RoomAvailability/GetAll");

            public Task<RoomAvailability?> GetRoomAvailabilityByIdAsync(int id)
                => GetAsync<RoomAvailability?>($"api/RoomAvailability/GetById/{id}");

            public Task<int> InsertRoomAvailabilityAsync(RoomAvailability ra)
                => PostAsync("api/RoomAvailability/Insert", ra);

            public Task<int> UpdateRoomAvailabilityAsync(RoomAvailability ra)
                => PutAsync("api/RoomAvailability/Update", ra);

            public Task<int> DeleteRoomAvailabilityAsync(int id)
                => DeleteAsync($"api/RoomAvailability/Delete/{id}");

            // ===================== Bookings =====================
            public Task<BookingList> GetAllBookingsAsync()
                => GetAsync<BookingList>("api/Bookings/GetAll");

            public Task<Booking?> GetBookingByIdAsync(int id)
                => GetAsync<Booking?>($"api/Bookings/GetById/{id}");

            public Task<int> InsertBookingAsync(Booking b)
                => PostAsync("api/Bookings/Insert", b);

            public Task<int> UpdateBookingAsync(Booking b)
                => PutAsync("api/Bookings/Update", b);

            public Task<int> DeleteBookingAsync(int id)
                => DeleteAsync($"api/Bookings/Delete/{id}");

            // ===================== Payments =====================
            public Task<PaymentList> GetAllPaymentsAsync()
                => GetAsync<PaymentList>("api/Payments/GetAll");

            public Task<Payment?> GetPaymentByIdAsync(int id)
                => GetAsync<Payment?>($"api/Payments/GetById/{id}");

            public Task<int> InsertPaymentAsync(Payment p)
                => PostAsync("api/Payments/Insert", p);

            public Task<int> UpdatePaymentAsync(Payment p)
                => PutAsync("api/Payments/Update", p);

            public Task<int> DeletePaymentAsync(int id)
                => DeleteAsync($"api/Payments/Delete/{id}");

            // ===================== Reviews =====================
            public Task<ReviewList> GetAllReviewsAsync()
                => GetAsync<ReviewList>("api/Reviews/GetAll");

            public Task<Review?> GetReviewByIdAsync(int id)
                => GetAsync<Review?>($"api/Reviews/GetById/{id}");

            public Task<int> InsertReviewAsync(Review r)
                => PostAsync("api/Reviews/Insert", r);

            public Task<int> UpdateReviewAsync(Review r)
                => PutAsync("api/Reviews/Update", r);

            public Task<int> DeleteReviewAsync(int id)
                => DeleteAsync($"api/Reviews/Delete/{id}");
        }
    }

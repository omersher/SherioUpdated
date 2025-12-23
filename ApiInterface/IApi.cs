using System.Threading.Tasks;
using Model;

namespace ApiInterface
{
    public interface IApi
    {
        // ---- Cities ----
        Task<CityList> GetAllCitiesAsync();
        Task<City?> GetCityByIdAsync(int id);
        Task<int> InsertCityAsync(City c);
        Task<int> UpdateCityAsync(City c);
        Task<int> DeleteCityAsync(int id);

        // ---- Users ----
        Task<UserList> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<int> InsertUserAsync(User u);
        Task<int> UpdateUserAsync(User u);
        Task<int> DeleteUserAsync(int id);

        // ---- Owners ----
        Task<OwnerList> GetAllOwnersAsync();
        Task<Owner?> GetOwnerByIdAsync(int id);
        Task<int> InsertOwnerAsync(Owner o);
        Task<int> UpdateOwnerAsync(Owner o);
        Task<int> DeleteOwnerAsync(int id);

        // ---- Hotels ----
        Task<HotelList> GetAllHotelsAsync();
        Task<Hotel?> GetHotelByIdAsync(int id);
        Task<int> InsertHotelAsync(Hotel h);
        Task<int> UpdateHotelAsync(Hotel h);
        Task<int> DeleteHotelAsync(int id);

        // ---- Rooms ----
        Task<RoomList> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task<int> InsertRoomAsync(Room r);
        Task<int> UpdateRoomAsync(Room r);
        Task<int> DeleteRoomAsync(int id);

        // ---- RoomImages ----
        Task<RoomImagesList> GetAllRoomImagesAsync();
        Task<RoomImage?> GetRoomImageByIdAsync(int id);
        Task<int> InsertRoomImageAsync(RoomImage ri);
        Task<int> UpdateRoomImageAsync(RoomImage ri);
        Task<int> DeleteRoomImageAsync(int id);

        // ---- RoomAvailability ----
        Task<RoomAvailabilityList> GetAllRoomAvailabilityAsync();
        Task<RoomAvailability?> GetRoomAvailabilityByIdAsync(int id);
        Task<int> InsertRoomAvailabilityAsync(RoomAvailability ra);
        Task<int> UpdateRoomAvailabilityAsync(RoomAvailability ra);
        Task<int> DeleteRoomAvailabilityAsync(int id);

        // ---- Bookings ----
        Task<BookingList> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<int> InsertBookingAsync(Booking b);
        Task<int> UpdateBookingAsync(Booking b);
        Task<int> DeleteBookingAsync(int id);

        // ---- Payments ----
        Task<PaymentList> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<int> InsertPaymentAsync(Payment p);
        Task<int> UpdatePaymentAsync(Payment p);
        Task<int> DeletePaymentAsync(int id);

        // ---- Reviews ----
        Task<ReviewList> GetAllReviewsAsync();
        Task<Review?> GetReviewByIdAsync(int id);
        Task<int> InsertReviewAsync(Review r);
        Task<int> UpdateReviewAsync(Review r);
        Task<int> DeleteReviewAsync(int id);
    }
}

    using Microsoft.AspNetCore.Mvc;
    using Model;
    using ViewModel;

    namespace SherioWebApplication.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class BookingsController : ControllerBase
        {
            // =====================
            // GET ALL (ADMIN)
            // =====================
            [HttpGet]
            public BookingList GetAll()
            {
                BookingDB db = new BookingDB();
                return db.SelectAll();
            }

            // =====================
            // GET BY HOTEL
            // api/Bookings/hotel/5
            // =====================
            [HttpGet("hotel/{hotelId}")]
            public BookingList GetByHotel(int hotelId)
            {
                BookingDB db = new BookingDB();
                return db.SelectByHotel(hotelId);
            }

            // =====================
            // GET BY ID
            // api/Bookings/3
            // =====================
            [HttpGet("{id}")]
            public Booking? GetById(int id)
            {
                return BookingDB.SelectById(id);
            }

            // =====================
            // INSERT
            // =====================
            [HttpPost]
            public int Insert([FromBody] Booking b)
            {
                if (b == null)
                    throw new Exception("Booking is NULL");

                if (b.UserID <= 0)
                    throw new Exception("UserID invalid");

                if (b.RoomID <= 0)
                    throw new Exception("RoomID invalid");

                if (b.StartDate == default || b.EndDate == default)
                    throw new Exception("Dates invalid");

                var db = new BookingDB();

                if (!db.IsRoomAvailable(b.RoomID, b.StartDate, b.EndDate))
                    throw new Exception("Room already booked");

                db.Insert(b);
                db.SaveChanges();
                return b.Id;
            }

            // =====================
            // UPDATE
            // =====================
            [HttpPut]
            public int Update([FromBody] BookingUpdateDto dto)
            {
                var booking = BookingDB.SelectById(dto.Id);
                if (booking == null) return 0;

                booking.AdultCount = dto.AdultCount;
                booking.ChildCount = dto.ChildCount;
                booking.Status = dto.Status;

                var db = new BookingDB();
                db.Update(booking);
                return db.SaveChanges();
            }

            // =====================
            // DELETE
            // =====================
            [HttpDelete("{id}")]
            public int Delete(int id)
            {
                var booking = BookingDB.SelectById(id);
                if (booking == null) return 0;

                var db = new BookingDB();
                db.Delete(booking);
                return db.SaveChanges();
            }
        }
    }
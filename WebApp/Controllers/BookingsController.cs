using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
        // GET BY HOTEL (OWNER)
        // api/Bookings/GetByHotel/5
        // =====================
        [HttpGet("{hotelId}")]
        public BookingList GetByHotel(int hotelId)
        {
            BookingDB db = new BookingDB();
            return db.SelectByHotel(hotelId);
        }

        // =====================
        // GET BY ID
        // api/Bookings/GetById/3
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
            var db = new BookingDB();
            db.Insert(b);
            return db.SaveChanges();
        }

        // =====================
        // UPDATE (DTO בלבד)
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
        // api/Bookings/Delete/5
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

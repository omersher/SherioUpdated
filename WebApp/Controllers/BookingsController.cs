using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookingsController : ControllerBase
    {
        [HttpGet]
        public BookingList GetAll()
        {
            BookingDB db = new BookingDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public Booking? GetById(int id) => BookingDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] Booking b)
        {
            var db = new BookingDB();
            db.Insert(b);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] Booking b)
        {
            var db = new BookingDB();
            db.Update(b);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var b = BookingDB.SelectById(id);
            if (b == null) return 0;
            var db = new BookingDB();
            db.Delete(b);
            return db.SaveChanges();
        }
    }
}

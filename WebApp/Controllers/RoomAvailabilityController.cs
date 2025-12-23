using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomAvailabilityController : ControllerBase
    {
        [HttpGet]
        public RoomAvailabilityList GetAll()
        {
            RoomAvailabilityDB db = new RoomAvailabilityDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public RoomAvailability? GetById(int id) => RoomAvailabilityDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] RoomAvailability ra)
        {
            var db = new RoomAvailabilityDB();
            db.Insert(ra);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] RoomAvailability ra)
        {
            var db = new RoomAvailabilityDB();
            db.Update(ra);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var ra = RoomAvailabilityDB.SelectById(id);
            if (ra == null) return 0;
            var db = new RoomAvailabilityDB();
            db.Delete(ra);
            return db.SaveChanges();
        }
    }
}

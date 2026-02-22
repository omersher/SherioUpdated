using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomAvailabilityController : ControllerBase
    {
        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public RoomAvailabilityList GetAll()
        {
            RoomAvailabilityDB db = new RoomAvailabilityDB();
            return db.SelectAll();
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public RoomAvailability? GetById(int id)
        {
            return RoomAvailabilityDB.SelectById(id);
        }

        // =========================
        // INSERT
        // =========================
        [HttpPost]
        public int Insert([FromBody] RoomAvailability ra)
        {
            if (ra == null)
                throw new Exception("RoomAvailability is null");

            if (ra.RoomID <= 0)
                throw new Exception("RoomID invalid");

            var db = new RoomAvailabilityDB();
            db.Insert(ra);
            db.SaveChanges();

            return ra.Id;
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut]
        public int Update([FromBody] RoomAvailability ra)
        {
            var db = new RoomAvailabilityDB();
            db.Update(ra);
            return db.SaveChanges();
        }

        // =========================
        // DELETE
        // =========================
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
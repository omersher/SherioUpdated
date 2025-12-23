using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public RoomList GetAll()
        {
            RoomDB db = new RoomDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public Room? GetById(int id) => RoomDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] Room r)
        {
            var db = new RoomDB();
            db.Insert(r);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] Room r)
        {
            var db = new RoomDB();
            db.Update(r);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var r = RoomDB.SelectById(id);
            if (r == null) return 0;
            var db = new RoomDB();
            db.Delete(r);
            return db.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HotelsController : ControllerBase
    {
        [HttpGet]
        public HotelList GetAll()
        {
            HotelsDB db = new HotelsDB();
            return db.SelectAll();
        }

        [HttpGet("{ownerId}")]
        public HotelList GetByOwnerId(int ownerId)
        {
            HotelsDB db = new HotelsDB();
            return db.SelectByOwnerId(ownerId);
        }

        [HttpGet("{id}")]
        public Hotel? GetById(int id) => HotelsDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] Hotel h)
        {
            var db = new HotelsDB();
            db.Insert(h);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] Hotel h)
        {
            var db = new HotelsDB();
            db.Update(h);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var h = HotelsDB.SelectById(id);
            if (h == null) return 0;
            var db = new HotelsDB();
            db.Delete(h);
            return db.SaveChanges();
        }
    }
}

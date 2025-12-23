using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomImagesController : ControllerBase
    {
        [HttpGet]
        public RoomImagesList GetAll()
        {
            RoomImagesDB db = new RoomImagesDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public RoomImage? GetById(int id) => RoomImagesDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] RoomImage ri)
        {
            var db = new RoomImagesDB();
            db.Insert(ri);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] RoomImage ri)
        {
            var db = new RoomImagesDB();
            db.Update(ri);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var ri = RoomImagesDB.SelectById(id);
            if (ri == null) return 0;
            var db = new RoomImagesDB();
            db.Delete(ri);
            return db.SaveChanges();
        }
    }
}

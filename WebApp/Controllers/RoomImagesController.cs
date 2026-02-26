using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomImagesController : ControllerBase
    {
        [HttpGet("{roomId}")]
        public RoomImagesList GetByRoomId(int roomId)
        {
            RoomImagesDB db = new RoomImagesDB();
            return db.SelectByRoomId(roomId);
        }

        [HttpPost]
        public int Insert([FromBody] RoomImageInsertDto dto)
        {
            RoomImagesDB db = new RoomImagesDB();

            var image = new RoomImage
            {
                Room = RoomDB.SelectById(dto.RoomId),
                ImageLink = dto.ImageUrl
            };

            db.Insert(image);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var ri = RoomImagesDB.SelectById(id);
            if (ri == null) return 0;

            RoomImagesDB db = new RoomImagesDB();
            db.Delete(ri);
            return db.SaveChanges();
        }
    }
}
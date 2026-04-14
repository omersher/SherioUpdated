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

        [HttpGet("{hotelId}")]
        public RoomList GetByHotel(int hotelId)
        {
            RoomDB db = new RoomDB();
            return db.SelectByHotel(hotelId);
        }

        [HttpGet("{id}")]
        public Room? GetById(int id)
        {
            return RoomDB.SelectById(id);
        }

        [HttpPost]
        public int Insert([FromBody] Room r)
        {
            var db = new RoomDB();
            db.Insert(r);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] RoomUpdateDto dto)
        {
            var room = RoomDB.SelectById(dto.Id);
            if (room == null) return 0;

            room.RoomName = dto.RoomName;
            room.AdultRate = dto.AdultRate;
            room.ChildRate = dto.ChildRate;
            room.Bedrooms = dto.Bedrooms;
            room.Bathrooms = dto.Bathrooms;
            room.HasKitchen = dto.HasKitchen;
            room.HasParking = dto.HasParking;
            room.HasBalcony = dto.HasBalcony;
            room.HasLivingRoom = dto.HasLivingRoom;
            room.TotalUnits = dto.TotalUnits;

            room.Hotel = HotelsDB.SelectById(dto.HotelId);

            var db = new RoomDB();
            db.Update(room);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var room = RoomDB.SelectById(id);
            if (room == null) return 0;

            var db = new RoomDB();
            db.Delete(room);
            return db.SaveChanges();
        }
    }
}
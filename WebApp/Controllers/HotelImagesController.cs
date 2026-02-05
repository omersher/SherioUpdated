using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelImagesController : ControllerBase
    {
        [HttpGet]
        public HotelImagesList GetAll()
        {
            HotelImagesDB db = new HotelImagesDB();
            return db.SelectAll();
        }

        [HttpGet("{id:int}")]
        public HotelImage? GetById(int id)
        {
            return HotelImagesDB.SelectById(id);
        }

        [HttpGet("ByHotel/{hotelId:int}")]
        public HotelImagesList GetByHotelId(int hotelId)
        {
            HotelImagesDB db = new HotelImagesDB();
            return db.SelectByHotelId(hotelId);
        }


        [HttpPost]
        public int Insert([FromBody] HotelImage hi)
        {
            HotelImagesDB db = new HotelImagesDB();
            db.Insert(hi);
            return db.SaveChanges();
        }


        [HttpPut]
        public int Update([FromBody] HotelImage hi)
        {
            HotelImagesDB db = new HotelImagesDB();
            db.Update(hi);
            return db.SaveChanges();
        }

        [HttpDelete("{id:int}")]
        public int Delete(int id)
        {
            var hi = HotelImagesDB.SelectById(id);
            if (hi == null) return 0;

            HotelImagesDB db = new HotelImagesDB();
            db.Delete(hi);
            return db.SaveChanges();
        }
    }
}

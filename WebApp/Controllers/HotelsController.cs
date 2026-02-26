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
        public int Update([FromBody] HotelUpdateDto dto)
        {
            var hotel = HotelsDB.SelectById(dto.Id);
            if (hotel == null) return 0;

            hotel.Name = dto.Name;
            hotel.PhoneNumber = dto.PhoneNumber;
            hotel.Email = dto.Email;
            hotel.WebSite = dto.WebSite;
            hotel.StreetAddress = dto.StreetAddress;
            hotel.StarRating = dto.StarRating;

            hotel.HasPool = dto.HasPool;
            hotel.HasGym = dto.HasGym;
            hotel.HasRestaurant = dto.HasRestaurant;

            // טיפול בתמונה
            if (!string.IsNullOrEmpty(dto.MainHotelImageLink) &&
                dto.MainHotelImageLink.Length > 300)
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(dto.MainHotelImageLink);

                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fullPath = Path.Combine(folderPath, fileName);

                    System.IO.File.WriteAllBytes(fullPath, imageBytes);

                    hotel.MainHotelImageLink = "/images/" + fileName;
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                hotel.MainHotelImageLink = dto.MainHotelImageLink;
            }

            var db = new HotelsDB();
            db.Update(hotel);
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

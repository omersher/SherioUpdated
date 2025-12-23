using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CityController : ControllerBase
    {
        [HttpGet]
        public CityList GetAll()
        {
            CityDB db = new CityDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public City? GetById(int id) => CityDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] City c)
        {
            var db = new CityDB();
            db.Insert(c);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] City c)
        {
            var db = new CityDB();
            db.Update(c);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var c = CityDB.SelectById(id);
            if (c == null) return 0;
            var db = new CityDB();
            db.Delete(c);
            return db.SaveChanges();
        }
    }
}

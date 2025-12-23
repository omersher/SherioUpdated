using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OwnersController : ControllerBase
    {
        [HttpGet]
        public OwnerList GetAll()
        {
            OwnerDB ownerDB = new OwnerDB();
            return ownerDB.SelectAll();
        }

        [HttpGet("{id}")]
        public Owner? GetById(int id) => OwnerDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] Owner o)
        {
            var db = new OwnerDB();
            db.Insert(o);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] Owner o)
        {
            var db = new OwnerDB();
            db.Update(o);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var o = OwnerDB.SelectById(id);
            if (o == null) return 0;
            var db = new OwnerDB();
            db.Delete(o);
            return db.SaveChanges();
        }
    }
}

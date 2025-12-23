using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReviewsController : ControllerBase
    {
        [HttpGet]
        public ReviewList GetAll()
        {
            ReviewDB db = new ReviewDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public Review? GetById(int id) => ReviewDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] Review r)
        {
            var db = new ReviewDB();
            db.Insert(r);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] Review r)
        {
            var db = new ReviewDB();
            db.Update(r);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var r = ReviewDB.SelectById(id);
            if (r == null) return 0;
            var db = new ReviewDB();
            db.Delete(r);
            return db.SaveChanges();
        }
    }
}

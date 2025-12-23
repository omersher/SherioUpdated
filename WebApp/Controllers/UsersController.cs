using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public UserList GetAll()
        {
            UserDB db = new UserDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public User? GetById(int id) => UserDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] User u)
        {
            var db = new UserDB();
            db.Insert(u);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] User u)
        {
            var db = new UserDB();
            db.Update(u);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var u = UserDB.SelectById(id);
            if (u == null) return 0;
            var db = new UserDB();
            db.Delete(u);
            return db.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace SherioWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PaymentsController : ControllerBase
    {
        [HttpGet]
        public PaymentList GetAll()
        {
            PaymentDB db = new PaymentDB();
            return db.SelectAll();
        }

        [HttpGet("{id}")]
        public Payment? GetById(int id) => PaymentDB.SelectById(id);

        [HttpPost]
        public int Insert([FromBody] Payment p)
        {
            var db = new PaymentDB();
            db.Insert(p);
            return db.SaveChanges();
        }

        [HttpPut]
        public int Update([FromBody] Payment p)
        {
            var db = new PaymentDB();
            db.Update(p);
            return db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var p = PaymentDB.SelectById(id);
            if (p == null) return 0;
            var db = new PaymentDB();
            db.Delete(p);
            return db.SaveChanges();
        }
    }
}

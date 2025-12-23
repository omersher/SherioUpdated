using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HotelList : List<Hotel>
    {
        public HotelList() { }
        public HotelList(IEnumerable<Hotel> list) : base(list) { }
		public HotelList(IEnumerable<BaseEntity> list) : base(list.Cast<Hotel>().ToList()) { }

	}
}

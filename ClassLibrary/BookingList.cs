using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class BookingList : List<Booking>
	{
		public BookingList() { }
		public BookingList(IEnumerable<Booking> list) : base(list) { }
		public BookingList(IEnumerable<BaseEntity> list) : base(list.Cast<Booking>().ToList()) { }
	}
}

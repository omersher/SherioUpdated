using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class RoomAvailabilityList : List<RoomAvailability>
	{
		public RoomAvailabilityList() { }
		public RoomAvailabilityList(IEnumerable<RoomAvailability> list) : base(list) { }
		public RoomAvailabilityList(IEnumerable<BaseEntity> list) : base(list.Cast<RoomAvailability>().ToList()) { }
	}
}

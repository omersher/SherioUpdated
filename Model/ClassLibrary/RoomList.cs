using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class RoomList : List<Room>
    {
        public RoomList() { }
        public RoomList(IEnumerable<Room> list) : base(list) { }
        public RoomList(IEnumerable<BaseEntity> list) : base(list.Cast<Room>().ToList()) { }
    }
}

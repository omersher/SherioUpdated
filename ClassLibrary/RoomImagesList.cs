using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class RoomImagesList : List<RoomImage>
    {
        public RoomImagesList() { }
        public RoomImagesList(IEnumerable<RoomImage> list) : base(list) { }
        public RoomImagesList(IEnumerable<BaseEntity> list) : base(list.Cast<RoomImage>().ToList()) { }
    }
}

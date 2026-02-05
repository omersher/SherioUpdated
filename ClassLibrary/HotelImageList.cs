using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class HotelImagesList : List<HotelImage>
    {
        public HotelImagesList() { }
        public HotelImagesList(IEnumerable<HotelImage> list) : base(list) { }
        public HotelImagesList(IEnumerable<BaseEntity> list) : base(list.Cast<HotelImage>().ToList()) { }
    }
}

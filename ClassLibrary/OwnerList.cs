using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class OwnerList : List<Owner>
	{
		public OwnerList() { }
		public OwnerList(IEnumerable<Owner> list) : base(list) { }
		public OwnerList(IEnumerable<BaseEntity> list) : base(list.Cast<Owner>().ToList()) { }
	}
}

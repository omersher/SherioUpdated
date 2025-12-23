using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class PaymentList : List<Payment>
	{
		public PaymentList() { }
		public PaymentList(IEnumerable<Payment> list) : base(list) { }
		public PaymentList(IEnumerable<BaseEntity> list) : base(list.Cast<Payment>().ToList()) { }
	}
}

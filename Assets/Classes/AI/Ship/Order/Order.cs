namespace AI
{
	public class Order
	{
		public EOrders e_order;
		public OrderAttribute attribute;

		public Order Clone()
		{
			return new Order
			{
				e_order = e_order,
				attribute = attribute.Clone()
			};
		}
	}
}
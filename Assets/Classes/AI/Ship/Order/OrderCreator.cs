using UnityEngine;

namespace AI.AIShip
{
	public static class OrderCreator
	{
		private static System.Random rnd = new System.Random();
		public static Order CreateOrder(EOrders order)
		{
			switch (order)
			{
				case EOrders.Patrol:
					return Patrool();

				default:
					throw new System.Exception("Error OrderCreator: unknown orderType");
			}
		}

		private static Order Patrool()
		{
			Order order = new Order()
			{
				e_order = EOrders.Patrol,
				attribute = new OrderAttribute
				{
					currentPosition = getRNDPosition(),
					destinationStep = getRNDPosition(),
					destinationOrder = getRNDPosition(),
				}
			};
			return order;
		}

		private static Vector3 getRNDPosition()
		{
			return new Vector3(
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				Settings.StarSystem.SYSTEM_SHIPS_LAYER
				);
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

namespace AI.AIShip
{
	public static class OrderCreator
	{
		private static System.Random rnd = new System.Random();
		public static Order CreateOrder(EOrders order, Ship ship)
		{
			switch (order)
			{
				case EOrders.Patrol:
					return Patrool(ship);

				case EOrders.DockPatrool:
					return DockPatrool();

				default:
					throw new System.Exception("Error OrderCreator: unknown orderType");
			}
		}

		private static Order Patrool(Ship ship)
		{
			Order order;

			if (ship.order == null)
			{
				order = new Order
				{
					e_order = EOrders.Patrol,
					destinationOrder = getRNDPosition(),
					currentPosition = getRNDPosition(),
					dock = null
				};
			} 
			else
			{
				order = ship.order.Clone();
				order.currentPosition = ship.order.currentPosition == default ? getRNDPosition() : ship.order.currentPosition;
				order.destinationOrder = getRNDPosition();
			}
			order.wayPoints = CalcWayPoints(ship, order);
			return order;
		}

		private static Order DockPatrool()
		{
			Order order = new Order() { };
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

		private static Queue<Vector3> CalcWayPoints(Ship ship, Order order)
		{
			Queue<Vector3> result = new Queue<Vector3>();

			Vector3 heading = order.destinationOrder - order.currentPosition;
			float distance = Vector3.Distance(order.destinationOrder, order.currentPosition);
			var direction = heading / distance;

			float nextDistance = 0;

			while (distance - nextDistance >= ship.param.maxSpeed)
			{
				nextDistance += ship.param.maxSpeed;
				Vector3 vec = order.currentPosition + direction * nextDistance;
				vec.z = Settings.StarSystem.SYSTEM_SHIPS_LAYER;
				result.Enqueue(vec);
			}
			result.Enqueue(order.destinationOrder);
			return result;
		}
	}
}

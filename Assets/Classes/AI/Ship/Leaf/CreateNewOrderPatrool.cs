using System.Collections.Generic;
using UnityEngine;

namespace AI.AIShip
{
	class CreateNewOrderPatrool : Leaf<Ship>
	{
		private static System.Random rnd = new System.Random();
		public override EStateNode Tick(Ship ship)
		{
			ship.order = CreateOrder(ship);
			return EStateNode.SUCCESS;
		}
		
		private Order CreateOrder(Ship ship)
		{
			ship.order.currentPosition = ship.order.destinationStep;
			Order order = ship.order.Clone();
			order.destinationOrder = GetNewPoint();
			order.wayPoints = CalcWayPoints(ship, order);
			return order;			
		}

		private Vector3 GetNewPoint()
		{
			return new Vector3(
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				Settings.StarSystem.SYSTEM_SHIPS_LAYER
				);
		}

		private Queue<Vector3> CalcWayPoints(Ship ship, Order order)
		{
			Queue<Vector3> result = new Queue<Vector3>();

			Vector3 heading = order.destinationOrder - ship.position;
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

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
			ship.order.attribute.currentPosition = ship.order.attribute.destinationStep;
			Order order = ship.order.Clone();
			order.attribute.destinationOrder = GetNewPoint();
			order.attribute.wayPoints = CalcWayPoints(ship, order.attribute);
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

		private Queue<Vector3> CalcWayPoints(Ship ship, OrderAttribute newAttribute)
		{
			Queue<Vector3> result = new Queue<Vector3>();

			Vector3 heading = newAttribute.destinationOrder - ship.position;
			float distance = Vector3.Distance(newAttribute.destinationOrder, newAttribute.currentPosition);
			var direction = heading / distance;

			float nextDistance = 0;

			while (distance - nextDistance >= ship.param.maxSpeed)
			{
				nextDistance += ship.param.maxSpeed;
				Vector3 vec = newAttribute.currentPosition + direction * nextDistance;
				vec.z = Settings.StarSystem.SYSTEM_SHIPS_LAYER;
				result.Enqueue(vec);
			}
			result.Enqueue(newAttribute.destinationOrder);
			return result;
		}
	}
}

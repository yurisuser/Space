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

				case EOrders.DockingTest:
					return DockTest(ship);

				case EOrders.JumpToSystem:
					return JumpToSystem(ship);

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

		private static Order DockTest(Ship ship)
		{
			Order order;
			if (ship.order == null)
			{
				order = new Order
				{
					e_order = EOrders.DockingTest,
					currentPosition = getRNDPosition(),
					dock = GetRandomDock(ship).controlCentre.dock,
				};
				order.wayPoints = CalcWayPoints(ship, order);
				order.destinationStep = order.wayPoints.Dequeue();
			}
			else
			{
				order = ship.order.Clone();
				order.e_order = EOrders.DockingTest;
				order.currentPosition = ship.order.currentPosition == default ? getRNDPosition() : ship.order.currentPosition;
				order.dock = GetRandomDock(ship).controlCentre.dock;
			}
			order.destinationOrder = order.dock.parent.position;
			order.wayPoints = CalcWayPoints(ship, order);
			order.destinationStep = order.wayPoints.Dequeue();
			return order;
		}

		public static Order JumpToSystem(Ship ship)
		{
			Order order;
			if (ship.order == null)
			{
				order = new Order
				{
					e_order = EOrders.JumpToSystem,
					currentPosition = getRNDPosition(),
				};
			}
			else
			{
				order = ship.order.Clone();
				order.currentPosition = ship.order.currentPosition == default ? getRNDPosition() : ship.order.currentPosition;
			}
			int distancesIndexDestination = rnd.Next(1, 3);
			order.destinationSystemIndex = Galaxy.DistancesSortedNear[ship.location.indexStarSystem][distancesIndexDestination].index;

			order.destinationOrder = Galaxy.DistancesSortedNear[ship.location.indexStarSystem][distancesIndexDestination].direction
				* Settings.StarSystem.RADIUS_HYPER_ENTRANCE;

			order.wayPoints = CalcWayPoints(ship, order);
			order.destinationStep = order.wayPoints.Dequeue();
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

		private static SubStarBody GetRandomDock(Ship ship)
		{
			List<SubStarBody> list = new List<SubStarBody>();
			for (int i = 0; i < Galaxy.StarSystemsArr[ship.location.indexStarSystem].planetSystemsArray.Length; i++)
			{
				if (Galaxy.StarSystemsArr[ship.location.indexStarSystem].planetSystemsArray[i].planet.controlCentre != null)
					list.Add(Galaxy.StarSystemsArr[ship.location.indexStarSystem].planetSystemsArray[i].planet);
				for (int u = 0; u < Galaxy.StarSystemsArr[ship.location.indexStarSystem].planetSystemsArray[i].moonsArray.Length; u++)
				{
					if (Galaxy.StarSystemsArr[ship.location.indexStarSystem].planetSystemsArray[i].moonsArray[u].controlCentre != null)
						list.Add(Galaxy.StarSystemsArr[ship.location.indexStarSystem].planetSystemsArray[i].moonsArray[u]);
				}
			}
			int index = rnd.Next(0, list.Count);
			return list[index];
		}
	}
}

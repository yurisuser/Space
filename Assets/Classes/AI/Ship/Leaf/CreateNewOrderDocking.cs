using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.AIShip
{
	class CreateNewOrderDocking : Leaf<Ship>
	{
		private static System.Random rnd = new System.Random();
		public override EStateNode Tick(Ship ship)
		{
			ship.order = CreateOrder(ship);
			return EStateNode.SUCCESS;
		}

		private Order CreateOrder(Ship ship)
		{
			SubStarBody body = GetRandomDock(ship);
			Order result = OrderCreator.CreateOrder(EOrders.DockPatrool);
			OrderAttribute attr = new OrderAttribute();
			attr.currentPosition = ship.order.attribute.currentPosition == null ? GetNewPoint(): ship.order.attribute.currentPosition;
			attr.destinationOrder = body.position;
			attr.wayPoints = CalcWayPoints(ship, attr);
			return result;
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
		private Vector3 GetNewPoint()
		{
			return new Vector3(
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				Settings.StarSystem.SYSTEM_SHIPS_LAYER
				);
		}

		private SubStarBody GetRandomDock(Ship ship)
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

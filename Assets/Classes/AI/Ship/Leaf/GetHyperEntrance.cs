using System;
using UnityEngine;

namespace AI.AIShip
{
	public class GetHyperEntrance : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.destinationOrder = CalcHyperEntrance(ship);
			return EStateNode.SUCCESS;
		}

		private Vector3 CalcHyperEntrance(Ship ship)
		{
			if (ship.location.indexStarSystem == ship.order.destinationSystemIndex) throw new Exception();

			return Galaxy.Distances[ship.location.indexStarSystem][ship.order.destinationSystemIndex].direction 
				* Settings.StarSystem.RADIUS_HYPER_ENTRANCE;
		}
	}
}

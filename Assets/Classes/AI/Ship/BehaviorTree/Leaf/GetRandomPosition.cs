using AI;
using UnityEngine;

namespace AIShip
{
	class GetRandomPosition : Leaf
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.attribute.currentPosition = ship.order.attribute.destinationOrder;
			ship.order.attribute.destinationOrder = GetNewPoint();
			return EStateNode.SUCCESS;
		}

		private Vector3 GetNewPoint()
		{
			return new Vector3(
				Random.Range(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				Random.Range(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				Settings.StarSystem.SYSTEM_SHIPS_LAYER
				);
		}
	}
}
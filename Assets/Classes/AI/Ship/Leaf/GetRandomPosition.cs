using UnityEngine;

namespace AI.AIShip
{
	class GetRandomPosition : Leaf<Ship>
	{
		private static System.Random rnd = new System.Random();
		public override EStateNode Tick(Ship ship)
		{
			ship.order.attribute.currentPosition = ship.order.attribute.destinationOrder;
			ship.order.attribute.destinationOrder = GetNewPoint();
			return EStateNode.SUCCESS;
		}

		private Vector3 GetNewPoint()
		{
			return new Vector3(
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				Settings.StarSystem.SYSTEM_SHIPS_LAYER
				);
		}
	}
}
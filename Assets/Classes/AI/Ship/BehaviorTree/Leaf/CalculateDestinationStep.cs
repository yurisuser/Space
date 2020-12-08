using AI;
using UnityEngine;

namespace AI
{
	public class CalculateDestinationStep : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			//TODO не создавать heading, direction как промежуточные значения, применяются по разу
			Vector3 heading = ship.order.attribute.destinationOrder - ship.position;
			float distance = Vector3.Distance(ship.order.attribute.destinationOrder, ship.position);
			var direction = heading / distance;

			if (distance <= ship.param.speed)
			{
				ship.order.attribute.destinationStep = ship.order.attribute.destinationOrder;
				return EStateNode.SUCCESS;
			}

			ship.order.attribute.destinationStep += direction * ship.param.speed;
			ship.order.attribute.destinationStep.z = Settings.StarSystem.SYSTEM_SHIPS_LAYER;

			return EStateNode.RUNNING;
		}
	}
}
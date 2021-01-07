using UnityEngine;
using AI.AIShip;

public static class ShipsCreator
{
	public static Ship[] CreateRandomShips(int shipsAmaunt, int indexStarsystem)
	{
		Ship[] shipsArr = new Ship[shipsAmaunt];
		for (int i = 0; i < shipsAmaunt; i++)
		{
			Vector3 position = getRNDPosition();
			Vector3 destination = getRNDPosition();
			Ship ship = new Ship
			{
				dest = getRNDPosition(),
				id = Galaxy.GetNextId(),
				name = Random.Range(0, 100).ToString(),
				param = Data.shipsParamArr[0],
				state = EShipState.inSpace,
				location = new Location {
					indexStarSystem = indexStarsystem,
					dock = null

				}
			};
			ship.order = OrderCreator.CreateOrder(AI.EOrders.DockingTest, ship);

			if (ship.id == 1018)
			{
				ship.order.currentPosition = new Vector3(-1000, 0, -3);
				ship.order.destinationOrder = new Vector3(1000,0, -3);
			}
			shipsArr[i] = ship;
		}
		return shipsArr;
	}

	private static Vector3 getRNDPosition()
	{
		return new Vector3(
			Random.Range(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
			Random.Range(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
			Settings.StarSystem.SYSTEM_SHIPS_LAYER
			);
	}
}

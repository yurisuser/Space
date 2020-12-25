using UnityEngine;
using AI;

public static class ShipsCreator
{
	public static Ship[] CreateRandomShips(int shipsAmaunt)
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
				order = new Order {
					e_order = EOrders.Patrol,
					attribute = new OrderAttribute
					{
						destinationOrder = destination,
						currentPosition = position,
					}
				},
				param = Data.shipsParamArr[0]
			};

			if (ship.id == 1018)
			{
				ship.order.attribute.currentPosition = new Vector3(-1000, 0, -3);
				ship.order.attribute.destinationOrder = new Vector3(1000,0, -3);
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

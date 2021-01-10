using System;

public static class Docker
{
	public static void ToDock(Ship ship)
	{
		ship.location.dock = ship.order.dock;
		ship.state = EShipState.docked;
		if (!Galaxy.StarSystemsArr[ship.location.indexStarSystem].shipsList.Remove(ship)) throw new Exception();
		ship.order.dock.AddToDock(ship);
	}

	public static void ToUndock(Ship ship)
	{
		ship.location.dock = null;
		ship.state = EShipState.undocking;
		Galaxy.StarSystemsArr[ship.location.indexStarSystem].shipsList.Add(ship);
		ship.order.dock.RemoveFromDock(ship);
	}
}

using System;
using System.Collections.Generic;

public static class Docker
{
	public static List<Ship> addingShipToSystemMapScene = new List<Ship>();
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
		if (Gmgr.currentScene == EScene.starSystem && ship.location.indexStarSystem == Gmgr.currentSystemIndex) AddToScene(ship);
	}

	private static void AddToScene(Ship ship) 
	{
		addingShipToSystemMapScene.Add(ship);
	}
}

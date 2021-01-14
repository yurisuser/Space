using System;
using System.Collections.Generic;
using UnityEngine;

public static class Docker
{
	private static System.Random rnd = new System.Random();
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
		if (Glob.currentScene == EScene.starSystem && ship.location.indexStarSystem == Glob.currentSystemIndex) AddToScene(ship);
	}

	public static void ToHyper(Ship ship)
	{
		ship.location.hyper = true;
		ship.state = EShipState.inHyper;
		if (!Galaxy.StarSystemsArr[ship.location.indexStarSystem].shipsList.Remove(ship)) throw new Exception();
		HyperSpace.AddToHyperSpace(ship);
	}

	public static void ToUnHyper(Ship ship)
	{
		ship.location.hyper = false;
		ship.location.indexStarSystem = ship.order.destinationSystemIndex;
		ship.order.currentPosition = GetRandomPoints();
		ship.order.destinationStep = ship.order.currentPosition;
		ship.order.destinationOrder = ship.order.currentPosition;
		ship.state = EShipState.unHypering;

		Galaxy.StarSystemsArr[ship.order.destinationSystemIndex].shipsList.Add(ship);

		if (Glob.currentScene == EScene.starSystem && ship.location.indexStarSystem == Glob.currentSystemIndex)
		{
			Debug.Log($"cur {ship.location.indexStarSystem} dest {ship.order.destinationSystemIndex}");
			AddToScene(ship);
		}
	}

	private static void AddToScene(Ship ship) 
	{
		addingShipToSystemMapScene.Add(ship);
	}

	private static Vector3 GetRandomPoints()
	{
		return new Vector3(
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
				Settings.StarSystem.SYSTEM_SHIPS_LAYER
				);
	}
}

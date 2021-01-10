using System;
using System.Collections.Generic;
using UnityEngine;

public class Dock
{
	public SubStarBody parent;
	public int limit;

	private List<Ship> dockedShips;

	public Dock(SubStarBody parent) {
		this.parent = parent;
		dockedShips = new List<Ship>();
	}

	public bool isDockEnabled()
	{
		return true;
	}
	public void AddToDock(Ship ship)
	{
		dockedShips.Add(ship);
		//if (!isDockEnabled()) return false;
		//int indexFreeDockSlot = GetIndexFreeDockingSlot();
		//if (indexFreeDockSlot >= 0)
		//{
		//	ship.state = EShipState.docked;
		//	ship.location.dock = parent.controlCentre.dock;
		//	dockedShips[indexFreeDockSlot] = ship;
		//	 if (!Galaxy.StarSystemsArr[ship.location.indexStarSystem].ShipsList.Remove(ship)) throw new Exception();
		//	return true;
		//}
		//ship.state = EShipState.inQueue;
		//queueDocking.Enqueue(ship);
		//return true;
	}

	public void RemoveFromDock(Ship ship)
	{
		if (dockedShips.Remove(ship)) throw new Exception();
		//int index = Array.FindIndex(dockedShips, x => x.id == ship.id);
		//ship.state = EShipState.inSpace;
		//Galaxy.StarSystemsArr[ship.location.indexStarSystem].ShipsList.Add(ship);
		//ship.position = new Vector3(parent.position.x, parent.position.y, Settings.StarSystem.SYSTEM_SHIPS_LAYER);
		//dockedShips[index] = null;
		//QueueToDock();
		//return ship;
	}
}

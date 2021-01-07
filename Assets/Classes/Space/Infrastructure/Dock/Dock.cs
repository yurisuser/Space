using System;
using System.Collections.Generic;
using UnityEngine;

public class Dock
{
	public SubStarBody parent;
	public Ship[] dockedShips;
	private Queue<Ship> queueDocking;
	public int limit;
	public Dock(SubStarBody parent, int limit) {
		this.parent = parent;
		dockedShips = new Ship[limit];
		queueDocking = new Queue<Ship>();
		this.limit = limit;
	}

	public bool isDockEnabled()
	{
		return true;
	}
	public bool AddToDock(Ship ship)
	{
		if (!isDockEnabled()) return false;
		int indexFreeDockSlot = GetIndexFreeDockingSlot();
		if (indexFreeDockSlot >= 0)
		{
			ship.state = EShipState.docked;
			ship.location.dock = parent.controlCentre.dock;
			dockedShips[indexFreeDockSlot] = ship;
			 if (!Galaxy.StarSystemsArr[ship.location.indexStarSystem].ShipsList.Remove(ship)) throw new Exception();
			return true;
		}
		ship.state = EShipState.inQueue;
		queueDocking.Enqueue(ship);
		return true;
	}

	public Ship RemoveFromDockToSpace(Ship ship)
	{
		int index = Array.FindIndex(dockedShips, x => x.id == ship.id);
		ship.state = EShipState.inSpace;
		Galaxy.StarSystemsArr[ship.location.indexStarSystem].ShipsList.Add(ship);
		ship.position = new Vector3(parent.position.x, parent.position.y, Settings.StarSystem.SYSTEM_SHIPS_LAYER);
		dockedShips[index] = null;
		QueueToDock();
		return ship;
	}

	private int GetIndexFreeDockingSlot()
	{
		for (int i = 0; i < dockedShips.Length; i++)
		{
			if (dockedShips[i] == default) return i;
		}
		return -1;
	}

	private bool QueueToDock()
	{
		if (queueDocking.Count <= 0) return false;
		int index = GetIndexFreeDockingSlot();
		if (index < 0) return false;
		dockedShips[index] = queueDocking.Dequeue();
		//if (QueueToDock()) return true; //need???
		return true;
	}
}

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
	public bool ToDocking(Ship ship)
	{
		if (!isDockEnabled()) return false;
		int indexFreeDockSlot = GetIndexFreeDockingSlot();
		if (indexFreeDockSlot >= 0)
		{
			ship.dockingState = EDockingState.docked;
			dockedShips[indexFreeDockSlot] = ship;
			return true;
		}
		ship.dockingState = EDockingState.inQueue;
		queueDocking.Enqueue(ship);
		return true;
	}

	public Ship Undocking(int index)
	{
		Ship ship = dockedShips[index];
		dockedShips[index] = null;
		ship.dockingState = EDockingState.undocked;
		ship.position = new Vector3(parent.position.x, parent.position.y, Settings.StarSystem.SYSTEM_SHIPS_LAYER);
		QueueToDock();
		return ship;
	}

	private int GetIndexFreeDockingSlot()
	{
		for (int i = 0; i < dockedShips.Length; i++)
		{
			if (dockedShips[i] == null) return i;
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

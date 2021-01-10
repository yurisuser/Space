using System;
using System.Collections.Generic;
using UnityEngine;

public class Dock
{
	public SubStarBody parent;
	public int limit;

	public List<Ship> dockedShips;

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
	}

	public void RemoveFromDock(Ship ship)
	{
		if (!dockedShips.Remove(ship)) throw new Exception();
	}
}

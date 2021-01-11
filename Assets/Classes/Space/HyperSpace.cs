using System.Collections.Generic;

public static class HyperSpace
{
	private static List<Ship> ships = new List<Ship>();

	public static int GetCount()
	{
		return ships.Count;
	}
	public static void Tick()
	{
		if (ships.Count == 0) return;
		for (int i = ships.Count - 1; i >= 0; i--)
		{
			RemoveFromHyper(ships[i]);
		}
		ships.Clear();
	}
	public static void AddToHyperSpace(Ship ship)
	{
		ships.Add(ship);
	}

	private static void RemoveFromHyper(Ship ship)
	{
		Docker.ToUnHyper(ship);
	}
}

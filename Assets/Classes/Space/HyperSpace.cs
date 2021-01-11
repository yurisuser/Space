using System.Collections.Generic;

public static class HyperSpace
{
	private static List<Ship> ships = new List<Ship>();
	private static List<int> ids = new List<int>();

	public static int GetCount()
	{
		return ships.Count;
	}
	public static void Tick()
	{
		if (ships.Count == 0) return;
		for (int i = ships.Count - 1; i >= 0; i--)
		{
			if (!ids.Exists( x => x == ships[i].id))
			{
				ids.Add(ships[i].id);
				continue;
			}
			RemoveFromHyper(ships[i]);
			ids.Remove(ships[i].id);
			ships.Remove(ships[i]);
		}
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

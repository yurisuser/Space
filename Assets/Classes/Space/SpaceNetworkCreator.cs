using System.Collections.Generic;

public static class SpaceNetworkCreator
{
	private static int maxDistance = 40;
	private static int minSectorSize = 10;
	private static int maxSectorSize = 20;
	private static int maxNeibours = 3;

	private static List<List<int>> nodeList;
	private static Sector[] sectorsArr;

	private static System.Random rnd = new System.Random();

	public static int[][] Create()
	{
		Init();
		bool key = true;
		while (key)
		{
			for (int i = 1; i < sectorsArr.Length; i++)
			{
				key = false;
				if (!sectorsArr[i].isActive) continue;
				ExpansionSector(i);
			}
		}
		return GetResultArray();
	}

	private static void ExpansionSector(int idSector)
	{
		for (int i = 0; i < sectorsArr[idSector].members.Count; i++)
		{
			if (!sectorsArr[idSector].members[i].isOpen) continue;
			int newId = GetValidNearSystem(sectorsArr[idSector].members[i]);
			if (newId == 0) continue;
			AddToSector(newId, idSector);
			AddConnection(newId, sectorsArr[idSector].members[i].idSystem);
			sectorsArr[idSector].members[i].isOpen = ValidationMember(sectorsArr[idSector].members[i]);
		}
	}

	private static int GetValidNearSystem(MemberSector member)
	{
		for (int i = 1; i < Galaxy.DistancesSortedNear.Length; i++)
		{
			if (Galaxy.DistancesSortedNear[member.idSystem][i].distance > maxDistance) break;
			if (!ValidToAdding(Galaxy.DistancesSortedNear[member.idSystem][i].index, member)) continue;
			return Galaxy.DistancesSortedNear[member.idSystem][i].index;
		}
		return 0;
	}

	private static void Init()
	{
		sectorsArr = new Sector[Galaxy.StarSystemsArr.Length / rnd.Next(minSectorSize, maxSectorSize) + 1];
		//initial connectArr
		nodeList = new List<List<int>>();
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			nodeList.Add(new List<int>());
		}
		//initial sectorArr
		for (int i = 1; i < sectorsArr.Length; i++)
		{
			Sector sector = new Sector
			{
				id = i,
				isActive = true,
				members = new List<MemberSector>()
			};
			MemberSector member = new MemberSector
			{
				idSector = i,
				idSystem = GetRandomNobodySystem(),
				isOpen = true
			};
			sector.members.Add(member);
			sectorsArr[i] = sector;
		}
	}

	private static void AddToSector(int idSystem, int idSector)
	{
		Galaxy.StarSystemsArr[idSystem].idSector = idSector;
		MemberSector member = new MemberSector
		{
			idSector = idSector,
			idSystem = idSystem,
			isOpen = true
		};
		sectorsArr[idSector].members.Add(member);
	}

	private static void AddConnection(int idA, int idB)
	{
		nodeList[idA].Add(idB);
		nodeList[idB].Add(idA);
	}

	private static bool ValidationMember(MemberSector member)
	{
		if (nodeList[member.idSector].Count >= maxNeibours) return false;
		return true;
	}

	private static bool ValidToAdding(int idSystem, MemberSector member)
	{
		if (Galaxy.StarSystemsArr[idSystem].idSector == 0) return true;
		if (Galaxy.StarSystemsArr[idSystem].idSector == member.idSector && nodeList[idSystem].Count < maxNeibours)
		{
			if (!IsConnectExist(idSystem, member.idSystem))
			{
				return true;
			}
		}
		return false;
	}
	private static bool IsConnectExist(int idA, int idB)
	{
		if (nodeList[idA].Contains(idB) || nodeList[idB].Contains(idB)) return true;
		return false;
	}

	private static int[][] GetResultArray()
	{
		int[][] result = new int[nodeList.Count][];
		for (int i = 0; i < nodeList.Count; i++)
		{
			result[i] = nodeList[i].ToArray();
		}
		Utilities.ShowMeObject(result);
		return result;
	}

	private static int GetRandomNobodySystem()
	{
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			var sys = Galaxy.StarSystemsArr[rnd.Next(1, Galaxy.StarSystemsArr.Length - 1)];
			if (sys.idSector == 0) return sys.id;
		}
		return 0;
	}

	private class Sector
	{
		public int id;
		public List<MemberSector> members;
		public bool isActive;
	}
	private class MemberSector
	{
		public int idSystem;
		public int idSector;
		public bool isOpen;
	}
}

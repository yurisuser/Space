using System.Collections.Generic;
using Delaunator;
//https://github.com/wolktocs/delaunator-csharp

public static class SpaceNetworkCreator
{
	private static int sectorsAmount = Settings.Galaxy.CONSTELLATION_AMMOUNT;

	private static List<List<int>> hypersList;
	private static Sector[] sectorsArr;

	public static int[][] Create()
	{
		CreateHypers();
		UnlinkPeriphery();
		LinkPeriphery();
		InitSectors();
		Expansion();
		RemoveInterSectorConnection();
		AddIntersectorConnection();
		return GetResultArray();
	}

	public static int[][] GetSectorIdsStar()
	{
		List<int[]> result = new List<int[]>();
		for (int i = 1; i < sectorsArr.Length; i++)
		{
			if (sectorsArr[i].members == null) continue;
			List<int> temp = new List<int>();
			for (int m = 0; m < sectorsArr[i].members.Count; m++)
			{
				temp.Add(sectorsArr[i].members[m].idSystem);
			}
			result.Add(temp.ToArray());
		}
		return result.ToArray();
	}

	private static void CreateHypers()
	{
		List<double> list = new List<double>();
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			list.Add(Galaxy.StarSystemsArr[i].position.x);
			list.Add(Galaxy.StarSystemsArr[i].position.y);
		}
		Triangulation tr = new Triangulation(list);

		hypersList = new List<List<int>>();
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			hypersList.Add(new List<int>());
		}

		for (int i = 0; i < tr.triangles.Count - 3; i += 3)
		{
			AddConnection(tr.triangles[i], tr.triangles[i + 1]);
			AddConnection(tr.triangles[i + 1], tr.triangles[i + 2]);
			AddConnection(tr.triangles[i + 2], tr.triangles[i]);
		}
	}

	private static void UnlinkPeriphery()
	{
		RemoveAllConnections(0);

		for (int i = 1; i < Galaxy.StarSystemsArr.Length; i++)
		{
			if (Galaxy.Distances[0][i].distance > Settings.Galaxy.GALAXY_RADIUS)
				RemoveAllConnections(i);
		}
	}

	private static void LinkPeriphery()
	{
		for (int i = 0; i < Galaxy.DistancesSortedNear[0].Length; i++)
		{
			if (Galaxy.DistancesSortedNear[0][i].distance < Settings.Galaxy.GALAXY_RADIUS) continue;
			AddOnceForClear(Galaxy.DistancesSortedNear[0][i].index);
		}
	}

	private static void AddOnceForClear(int id)
	{
		for (int i = 1; i < Galaxy.DistancesSortedNear[id].Length; i++)
		{
			int idNeib = Galaxy.DistancesSortedNear[id][i].index;
			if (hypersList[idNeib].Count > 0)
			{
				AddConnection(id, idNeib);
				break;
			}
			if (Galaxy.Distances[0][idNeib].distance <= Settings.Galaxy.GALAXY_RADIUS)
			{
				AddConnection(id, Galaxy.DistancesSortedNear[id][i].index);
				break;
			}
		}
	}

	private static void InitSectors()
	{
		sectorsArr = new Sector[sectorsAmount];
		for (int i = 1; i < sectorsArr.Length; i++)
		{
			Sector sector = new Sector
			{
				id = i,
				isOpen = true,
				members = new List<MemberSector>()
			};
			MemberSector member = new MemberSector
			{
				idSector = i,
				idSystem = i,
				isOpen = true
			};
			Galaxy.StarSystemsArr[member.idSystem].idSector = member.idSector;
			sector.members.Add(member);
			sectorsArr[i] = sector;
		}
	}

	private static void Expansion()
	{
		bool key = true;
		while (key)
		{
			key = false;
			for (int i = 1; i < sectorsArr.Length; i++)
			{
				if (!sectorsArr[i].isOpen) continue;
				ExpansionSector(sectorsArr[i]);
				key = true;
			}
		}
	}

	private static void ExpansionSector(Sector sector)
	{
		for (int i = 0; i < sector.members.Count; i++)
		{
			if (!sector.members[i].isOpen) continue;
			int idNewMember = GetNewSystem(sector.members[i]);
			if (idNewMember == 0)
			{
				sector.members[i].isOpen = false;
				continue;
			}
			sector.members.Add(CreateNewMember(idNewMember, sector.id));
			return;
		}
		sector.isOpen = false;
	}

	private static int GetNewSystem(MemberSector member)
	{
		for (int i = 0; i < hypersList[member.idSystem].Count; i++)
		{
			int neibourId = hypersList[member.idSystem][i];
			if (Galaxy.StarSystemsArr[neibourId].idSector == 0)
			{
				return neibourId;
			}
		}
		return 0;
	}	

	private static MemberSector CreateNewMember(int idSystem, int idSector)
	{
		Galaxy.StarSystemsArr[idSystem].idSector = idSector;
		MemberSector member = new MemberSector
		{
			idSector = idSector,
			idSystem = idSystem,
			isOpen = true
		};
		return member;
	}

	private static void AddConnection(int idA, int idB)
	{
		if (!hypersList[idA].Contains(idB))
			hypersList[idA].Add(idB);
		if (!hypersList[idB].Contains(idA))
			hypersList[idB].Add(idA);
	}

	private static void AddIntersectorConnection()
	{
		for (int i = 1; i < sectorsArr.Length; i++)
		{
			for (int k = 0; k < sectorsArr[i].bestNeibourSectorMembersList.Count; k++)
			{
				AddConnection(
					sectorsArr[i].bestNeibourSectorMembersList[k].idOwnSys,
					sectorsArr[i].bestNeibourSectorMembersList[k].idExternSys
					);
			}			
		}
	}

	private static void RemoveConnection(int idA, int idB)
	{
		if (hypersList[idA].FindAll(x => x == idB).Count > 1) throw new System.Exception();
		if (hypersList[idB].FindAll(x => x == idA).Count > 1) throw new System.Exception();
		if (!hypersList[idA].Remove(idB)) throw new System.Exception();
		if (!hypersList[idB].Remove(idA)) throw new System.Exception();
		if (hypersList[idA].FindAll(x => x == idB).Count > 0) throw new System.Exception();
		if (hypersList[idB].FindAll(x => x == idA).Count > 0) throw new System.Exception();
	}

	private static void RemoveAllConnections(int id)
	{
		for (int i = 0; i < hypersList[id].Count; i++)
		{
			var idNear = hypersList[id][i];
			if (!hypersList[idNear].Remove(id))
			{
				throw new System.Exception();
			}
		}
		hypersList[id].Clear();
	}

	private static void RemoveInterSectorConnection()
	{
		for (int i = 0; i < hypersList.Count; i++)
		{
			for (int k = hypersList[i].Count - 1; k >= 0; k--)
			{
				int targetId = hypersList[i][k];
				if (Galaxy.StarSystemsArr[i].idSector != Galaxy.StarSystemsArr[targetId].idSector)
				{
					SaveBestConnection(i, targetId);
					RemoveConnection(i, targetId);
				}
			}
			if (Galaxy.StarSystemsArr[i].idSector == 0) RemoveAllConnections(i);
		}
	}

	private static void SaveBestConnection(int idSysA, int idSysB)
	{
		Sector secA = sectorsArr[Galaxy.StarSystemsArr[idSysA].idSector];
		Sector secB = sectorsArr[Galaxy.StarSystemsArr[idSysB].idSector];

		secA.AddBest(idSysA, idSysB);
		secB.AddBest(idSysB, idSysA);
	}

	private static int[][] GetResultArray()
	{
		int[][] result = new int[hypersList.Count][];
		for (int i = 0; i < hypersList.Count; i++)
		{
			result[i] = hypersList[i].ToArray();
		}
		return result;
	}

	private class Sector
	{
		public int id;
		public List<MemberSector> members;
		public bool isOpen;
		public List<BestNeibourSectorMember> bestNeibourSectorMembersList = new List<BestNeibourSectorMember>();

		public void AddBest(int idOwn, int idExtern)
		{
			int oldIndex = bestNeibourSectorMembersList.FindIndex(x => x.idNeibourSector == Galaxy.StarSystemsArr[idExtern].idSector);
			var addingBest = new BestNeibourSectorMember()
			{
				idNeibourSector = Galaxy.StarSystemsArr[idExtern].idSector,
				idOwnSys = idOwn,
				idExternSys = idExtern,
				distance = Galaxy.Distances[idOwn][idExtern].distance
			};
			if (oldIndex < 0)
			{
				bestNeibourSectorMembersList.Add(addingBest);
				return;
			}
			if (bestNeibourSectorMembersList[oldIndex].distance > Galaxy.Distances[idOwn][idExtern].distance)
			{
				bestNeibourSectorMembersList[oldIndex] = addingBest;
			}
		}
	}
	private class MemberSector
	{
		public int idSystem;
		public int idSector;
		public bool isOpen;
	}

	private struct BestNeibourSectorMember
	{
		public int idNeibourSector;
		public int idOwnSys;
		public int idExternSys;
		public float distance;
	}
}

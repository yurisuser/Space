using System.Collections.Generic;
using UnityEngine;
using Delaunator;

public static class SpaceNetworkCreator
{
	private static int minSectorSize = 5;
	private static int maxSectorSize = 10;
	private static int sectorsAmount = Settings.Galaxy.STARS_AMMOUNT / ((minSectorSize + maxSectorSize) / 2);

	private static List<List<int>> hypersList;
	private static Sector[] sectorsArr;

	private static System.Random rnd = new System.Random();

	public static int[][] Create()
	{
		CreateHypers();
		UnlinkPeriphery();
		LinkPeriphery();
		InitSectors();
		Expansion();
		CheckTest();
		RemoveInterSectorConnection();
		//RemoveIntesectorConnection();
		return GetResultArray();
	}

	private static void CheckTest()
	{
		for (int i = 0; i < hypersList.Count; i++)
		{
			for (int j = 0; j < hypersList[i].Count; j++)
			{
				var e = hypersList[i].FindAll(x => x == hypersList[i][j]);
				if (e.Count > 1) {
					throw new System.Exception();
				}
			}
		}
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
			//if (nodeList[i].Count > 0) continue;
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
		//initial sectorArr
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
			//if (sector.members.Count >= maxSectorSize)
			//{
			//	sector.isOpen = false;
			//}
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
				if (Galaxy.StarSystemsArr[i].idSector != Galaxy.StarSystemsArr[targetId].idSector) RemoveConnection(i, targetId);
			}
			if (Galaxy.StarSystemsArr[i].idSector == 0) RemoveAllConnections(i);
		}
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
	}
	private class MemberSector
	{
		public int idSystem;
		public int idSector;
		public bool isOpen;
	}

	private static void DeleteIntersections()
	{
		int positive = 0;
		int negative = 0;
		for (int curIndex = hypersList.Count - 1; curIndex >= 0; curIndex--)
		{//sysId = curindex

			for (int neibIndex = hypersList[curIndex].Count - 1; neibIndex >= 0; neibIndex--)
			{//current nodelist[curIndex][neibourIndex]
				int idNeibour = hypersList[curIndex][neibIndex];

				for (int neibNeibIndex = hypersList[idNeibour].Count - 1; neibNeibIndex >= 0; neibNeibIndex--)
				{ //line from[currSys] to [neibSys] intersect  from [neibSys] to [neibNeibSys]
					int idNeibourNeibour = hypersList[idNeibour][neibNeibIndex];
					bool result = LineLineIntersection(
						Galaxy.StarSystemsArr[curIndex].position,
						Galaxy.StarSystemsArr[idNeibour].position,
						Galaxy.StarSystemsArr[idNeibour].position,
						Galaxy.StarSystemsArr[idNeibourNeibour].position
						);
					if (result) positive++; else negative++;
					if (result)
					{
						hypersList[idNeibour].Remove(idNeibourNeibour);
						//nodeList[idNeibourNeibour].Remove(idNeibour);
					}
				}
			}
		}
		Debug.Log($"+ {positive} - {negative}");
	}

	public static bool LineLineIntersection( Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
	{

		Vector3 lineVec3 = linePoint2 - linePoint1;
		Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);

		float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

		//is coplanar, and not parrallel
		if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

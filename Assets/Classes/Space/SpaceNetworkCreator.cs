using System.Collections.Generic;
using UnityEngine;
using Delaunator;

public static class SpaceNetworkCreator
{
	private static int maxDistance = 70;
	private static int minSectorSize = 10;
	private static int maxSectorSize = 20;
	private static int maxNeibours = 4;

	private static List<List<int>> nodeList;
	private static Sector[] sectorsArr;

	private static System.Random rnd = new System.Random();

	public static int[][] Create()
	{
		List<double> list = new List<double>();
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			list.Add(Galaxy.StarSystemsArr[i].position.x);
			list.Add(Galaxy.StarSystemsArr[i].position.y);
		}
		Triangulation tr = new Triangulation(list);

		nodeList = new List<List<int>>();
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			nodeList.Add( new List<int>());
		}

		for (int i = 0; i < tr.triangles.Count -3; i += 3)
		{
			AddConnection(tr.triangles[i], tr.triangles[i + 1]);
			AddConnection(tr.triangles[i + 1], tr.triangles[i + 2]);
			AddConnection(tr.triangles[i + 2], tr.triangles[i]);
		}

		DeleteUnhanding();
		LinkUnlinkedPereferal();

		return GetResultArray();
	}

	private static void DeleteUnhanding()
	{
		DeleteLinks(0);

		for (int i = 1; i < Galaxy.StarSystemsArr.Length; i++)
		{
			if (Galaxy.Distances[0][i].distance > Settings.Galaxy.GALAXY_RADIUS)
				DeleteLinks(i);
		}
	}

	private static void DeleteLinks(int idStar)
	{
		for (int i = 0; i < nodeList[idStar].Count; i++)
		{
			var idNear = nodeList[idStar][i];
			if( !nodeList[idNear].Remove(idStar))
			{
				throw new System.Exception();
			}
		}
		nodeList[idStar].Clear();
	}

	private static void LinkUnlinkedPereferal()
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
			if (nodeList[idNeib].Count > 0)
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

	private static void ExpansionSector(Sector sector)
	{
		for (int i = 0; i < sector.members.Count; i++)
		{
			if (!sector.members[i].isOpen) continue;

			int newId = GetValidNearSystem(sector.members[i]);
			if (newId == 0) continue; //not central black hole

			if (Galaxy.StarSystemsArr[newId].idSector == 0)
			{
			}
				var newMember = CreateNewMember(newId, sector.id);
				sector.members.Add(newMember);
			AddConnection(newId, sector.members[i].idSystem);

			if (GetPossibleConnections(sector.members[i]) <= 0)
				sector.members[i].isOpen = false;
		}
	}

	private static void Init()
	{
		int SectorsAmount = Galaxy.StarSystemsArr.Length / rnd.Next(minSectorSize, maxSectorSize) + 1;
		SectorsAmount = 50;
		sectorsArr = new Sector[SectorsAmount];
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
		if (!nodeList[idA].Contains(idB))
			nodeList[idA].Add(idB);
		if (!nodeList[idB].Contains(idA))
			nodeList[idB].Add(idA);
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

	private static int GetPossibleConnections(MemberSector member)
	{
		if (nodeList[member.idSystem].Count >= maxNeibours) return 0;

		int possibleLines = 0;
		for (int i = 0; i < maxNeibours; i++)
		{
			if (Galaxy.DistancesSortedNear[member.idSystem][i].distance > maxDistance) break;
			possibleLines++;
		}
		if (possibleLines - nodeList[member.idSystem].Count < 0) throw new System.Exception();
		return possibleLines - nodeList[member.idSystem].Count;
	}

	private static bool ValidToAdding(int idSystem, MemberSector member)
	{
		if (Galaxy.StarSystemsArr[idSystem].idSector == 0) return true;
		if (Galaxy.StarSystemsArr[idSystem].idSector != member.idSector) return false;
		if (Galaxy.StarSystemsArr[idSystem].idSector == member.idSector && GetPossibleConnections(member) > 0)
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
		if (nodeList[idA].Contains(idB) || nodeList[idB].Contains(idA)) return true;
		return false;
	}

	private static int[][] GetResultArray()
	{
		int[][] result = new int[nodeList.Count][];
		for (int i = 0; i < nodeList.Count; i++)
		{
			result[i] = nodeList[i].ToArray();
		}
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

	private static void DeleteIntersections()
	{
		int positive = 0;
		int negative = 0;
		for (int curIndex = nodeList.Count - 1; curIndex >= 0; curIndex--)
		{//sysId = curindex

			for (int neibIndex = nodeList[curIndex].Count - 1; neibIndex >= 0; neibIndex--)
			{//current nodelist[curIndex][neibourIndex]
				int idNeibour = nodeList[curIndex][neibIndex];

				for (int neibNeibIndex = nodeList[idNeibour].Count - 1; neibNeibIndex >= 0; neibNeibIndex--)
				{ //line from[currSys] to [neibSys] intersect  from [neibSys] to [neibNeibSys]
					int idNeibourNeibour = nodeList[idNeibour][neibNeibIndex];
					bool result = LineLineIntersection(
						Galaxy.StarSystemsArr[curIndex].position,
						Galaxy.StarSystemsArr[idNeibour].position,
						Galaxy.StarSystemsArr[idNeibour].position,
						Galaxy.StarSystemsArr[idNeibourNeibour].position
						);
					if (result) positive++; else negative++;
					if (result)
					{
						nodeList[idNeibour].Remove(idNeibourNeibour);
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

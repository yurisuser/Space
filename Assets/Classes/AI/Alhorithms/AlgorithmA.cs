using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{
	public static class AlgorithmA
	{
		private static List<PathNode> closedSet;
		private static List<PathNode> openSet;
		private static PathNode startNode;
		private static int goalId;
		private static int startId;
		private static float _range;
		public static int[] GalaxyPathFinder(int start_Id, int goal_Id, float range, bool isJump)
		{
			closedSet = new List<PathNode>();
			openSet = new List<PathNode>();
			goalId = goal_Id;
			startId = start_Id;
			_range = range;

			startNode = new PathNode
			{
				idStar = start_Id,
				pathLengthFromStart = 0,
				cameFrom = default,
				heuristicEstimatePathLength = Galaxy.Distances[start_Id][goal_Id].distance,
			};

			openSet.Add(startNode);

			while(openSet.Count > 0)
			{
				var currentNode = openSet.OrderBy(node => node.estimateFullPathLength).First();
				if (currentNode.idStar == Galaxy.StarSystemsArr[goal_Id].id) return GetPathForNode(currentNode).ToArray();

				openSet.Remove(currentNode);
				closedSet.Add(currentNode);

				var collection = isJump == true ? GetNeighbourJump(currentNode) : GetNeibourTunnel(currentNode);
				foreach (var neighbourNode in collection)
				{
					if (closedSet.Count(node => node.idStar == neighbourNode.idStar) > 0) continue;

					var openNode = openSet.FirstOrDefault(node => node.idStar == neighbourNode.idStar);

					if (openNode == null)
					{
						openSet.Add(neighbourNode);
					} else {
						if (openNode.pathLengthFromStart > neighbourNode.pathLengthFromStart)
						{
							openNode.cameFrom = currentNode;
							openNode.pathLengthFromStart = neighbourNode.pathLengthFromStart;
						}
					}
				}
			}
			return new int[0];
		}

		private static List<int> GetPathForNode(PathNode pathNode)
		{
			var result = new List<int>();
			var currentNode = pathNode;
			while (currentNode != null)
			{
				result.Add(currentNode.idStar);
				currentNode = currentNode.cameFrom;
			}
			result.Reverse();
			return result;
		}

		private static List<PathNode> GetNeibourTunnel(PathNode pathNode)
		{
			var result = new List<PathNode>();
			for (int i = 0; i < Galaxy.HyperTunnels[pathNode.idStar].Length; i++)
			{
				var neibourNode = new PathNode
				{
					idStar = Galaxy.HyperTunnels[pathNode.idStar][i],
					cameFrom = pathNode,
					pathLengthFromStart = pathNode.pathLengthFromStart
						+ GetDistanceBetween(pathNode.idStar, Galaxy.DistancesSortedNear[pathNode.idStar][i].index), //глюк может быть тут
					heuristicEstimatePathLength = GetDistanceBetween(pathNode.idStar, Galaxy.DistancesSortedNear[pathNode.idStar][i].index)
				};
				result.Add(neibourNode);
			}
			return result;
		}

		private static List<PathNode> GetNeighbourJump(PathNode pathNode)
		{
			var result = new List<PathNode>();
			for (int i = 1; i < Settings.Galaxy.STARS_AMMOUNT - 1; i++)
			{
				var neibour = Galaxy.DistancesSortedNear[pathNode.idStar][i];
				if (neibour.distance > _range)
				{
					break;
				}

				var neibourNode = new PathNode
				{
					idStar = Galaxy.DistancesSortedNear[pathNode.idStar][i].index,
					cameFrom = pathNode,
					pathLengthFromStart = pathNode.pathLengthFromStart
						+ GetDistanceBetween(pathNode.idStar, Galaxy.DistancesSortedNear[pathNode.idStar][i].index), //глюк может быть тут
					heuristicEstimatePathLength = Galaxy.Distances[neibour.index][goalId].distance
				};
				result.Add(neibourNode);
			}
			return result;
		}

		private static float GetDistanceBetween(int a, int b)
		{
			//return Galaxy.Distances[a][b].distance;
			return 1;
		}

		private class PathNode 
		{
			public int idStar;
			public float pathLengthFromStart;
			public PathNode cameFrom;
			public float heuristicEstimatePathLength;
			public float estimateFullPathLength
			{
				get => pathLengthFromStart + heuristicEstimatePathLength;
			}

		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateGalaxy : SceneState
{
	private static readonly string folderNameHyperTunnels = "hyperTunnels";
	private string tagStar = "Star";
	public override void DrawScene()
	{
		GameObject folder = new GameObject { name = tagStar };
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			GameObject star = GameObject.Instantiate(
				PrefabService.StarsGalaxyMap[Galaxy.StarSystemsArr[i].star.type],
				Galaxy.StarSystemsArr[i].position,
				Quaternion.identity);
			star.name = i.ToString();
			star.transform.SetParent(folder.transform);
			star.GetComponent<StarGalaxyMap>().dist = Galaxy.StarSystemsArr[i].oldX;
			star.GetComponent<StarGalaxyMap>().idSystem = i;
		}
		DrawLines(Galaxy.HyperTunnels, folderNameHyperTunnels, Settings.Galaxy.COLOR_HYPER_TUNNEL);
	}

	private void DrawLines(int[][] arr, string folderName, Color color)
	{
		var networkFolder = GameObject.Find(folderName);
		if (networkFolder != null) GameObject.Destroy(networkFolder);
		GameObject folder = new GameObject { name = folderName };

		List<GameObject> lines = new List<GameObject>();
		for (int i = 0; i < arr.Length; i++)
		{
			for (int q = 0; q < arr[i].Length; q++)
			{
				if (i > arr[i][q]) continue; //drop duble lines
				var go = new GameObject();
				go.transform.position = Galaxy.StarSystemsArr[Galaxy.HyperTunnels[i][q]].position;
				go.AddComponent<LineRenderer>();
				LineRenderer lr = go.GetComponent<LineRenderer>();
				lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
				lr.startColor = lr.endColor = color;
				lr.startWidth = lr.endWidth = 0.5f;
				lr.SetPosition(0, Galaxy.StarSystemsArr[i].position);
				lr.SetPosition(1, Galaxy.StarSystemsArr[arr[i][q]].position);
				go.transform.SetParent(folder.transform);
				lines.Add(go);
			}
		}
	}
}
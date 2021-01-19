using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateGalaxy : SceneState
{
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
			star.GetComponent<StarGalaxyMap>().starsArrayIndex = i;
		}
		DrawLine();
	}

	private void DrawLine()
	{
		var networkFolder = GameObject.Find("network");
		if (networkFolder != null) GameObject.Destroy(networkFolder);
		GameObject folder = new GameObject { name = "way" };


		List<GameObject> lines = new List<GameObject>();
		for (int i = 0; i < Galaxy.NetworkNodes.Length; i++)
		{
			for (int q = 0; q < Galaxy.NetworkNodes[i].Length; q++)
			{
				if (i > Galaxy.NetworkNodes[i][q]) continue; //drop duble lines
				var go = new GameObject();
				go.transform.position = Galaxy.StarSystemsArr[Galaxy.NetworkNodes[i][q]].position;
				go.AddComponent<LineRenderer>();
				LineRenderer lr = go.GetComponent<LineRenderer>();
				lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
				lr.startColor = lr.endColor = new Color(.3f, .6f, .2f, .5f);
				lr.startWidth = lr.endWidth = 0.5f;
				lr.SetPosition(0, Galaxy.StarSystemsArr[i].position);
				lr.SetPosition(1, Galaxy.StarSystemsArr[Galaxy.NetworkNodes[i][q]].position);
				go.transform.SetParent(folder.transform);
				lines.Add(go);
			}
		}
	}
}
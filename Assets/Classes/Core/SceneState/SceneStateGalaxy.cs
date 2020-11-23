using System;
using UnityEngine;

public class SceneStateGalaxy : SceneState
{
	private string tagStar = "Star";
	Camera cam;
	public override void DrawScene()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		GameObject folder = new GameObject { name = tagStar };
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			GameObject star = GameObject.Instantiate(
				PrefabService.StarsGalaxyMap[Galaxy.StarSystemsArr[i].star.type],
				Galaxy.StarSystemsArr[i].position,
				Quaternion.identity);
			star.name = i.ToString();
			star.transform.SetParent(folder.transform);
			star.GetComponent<StarGalaxyMap>().dist = Galaxy.StarSystemsArr[i].galaxyHandDistance;
		}
	}
}
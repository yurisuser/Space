using UnityEngine;
using System;

public class StarGalaxyMap : MonoBehaviour
{
	public int starsArrayIndex = 0;
	public float dist;
	Camera cam;

	private void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
	}
	private void OnMouseDown()
	{
		if(Utilities.CheckRaycastWithoutUI(cam, gameObject.name))
		{
			Gmgr.gmgr.LoadSceneStarSystem(Convert.ToInt32(starsArrayIndex));
		}
	}
}

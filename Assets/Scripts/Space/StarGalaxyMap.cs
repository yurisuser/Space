using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class StarGalaxyMap : MonoBehaviour
{
	Camera cam;
	public float dist;

	private void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
	}
	private void OnMouseDown()
	{
		if(Utilities.CheckRaycastWithoutUI(cam, gameObject.name))
		{
			Gmgr.gmgr.LoadSceneStarSystem(Convert.ToInt32(gameObject.name));
		}
	}
}

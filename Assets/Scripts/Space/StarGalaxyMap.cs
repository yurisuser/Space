using UnityEngine;
using System;
using UnityEngine.EventSystems;

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
		if (EventSystem.current.IsPointerOverGameObject()) return;
		Gmgr.gmgr.LoadSceneStarSystem(Convert.ToInt32(starsArrayIndex));
	}
}

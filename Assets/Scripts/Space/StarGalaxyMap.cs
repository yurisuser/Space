using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class StarGalaxyMap : MonoBehaviour
{
	public int starsArrayIndex = 0;
	public float dist;

	private float mouseOverTime = 0;
	private bool mouseOverActive = true;
	private void Start()
	{
	}
	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		Debug.Log($"Enter to {starsArrayIndex} system");
		Gmgr.gmgr.LoadSceneStarSystem(Convert.ToInt32(starsArrayIndex));
	}
	private void OnMouseOver()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		if (mouseOverActive == false) return;
		mouseOverTime += Time.deltaTime;
		if (mouseOverTime < Settings.UI.MOUSE_OVER_TIME) return;

		ShowSystemIndex();

		mouseOverTime = 0;
		//mouseOverActive = false;
	}

	private void ShowSystemIndex()
	{
		Debug.Log(starsArrayIndex);
	}
}

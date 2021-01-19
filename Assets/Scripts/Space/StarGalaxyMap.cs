using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class StarGalaxyMap : MonoBehaviour
{
	public int idSystem = 0;
	public float dist;

	private float mouseOverTime = 0;
	private bool mouseOverActive = true;
	private void Start()
	{
	}
	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		Debug.Log($"Enter to {idSystem} system");
		Gmgr.gmgr.LoadSceneStarSystem(Convert.ToInt32(idSystem));
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
		Debug.Log($"{idSystem}  id sector: {Galaxy.StarSystemsArr[idSystem].idSector}");
	}
}

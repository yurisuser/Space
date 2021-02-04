﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemViewerPlanetScr : MonoBehaviour
{
    public Planet planet;
	public Vector3 oldScale;
	public List<GameObject> systemObjects;

	private float scaleSpeed = 5;
	private float largeSize = 3;
	private Vector3 largeScale;
	private bool isToLarge = false;
	private bool isToNormal = false;
	private void OnMouseUp()
	{
		largeScale = oldScale * largeSize;
		for (int i = 0; i < systemObjects.Count; i++)
		{
			systemObjects[i].GetComponent<SystemViewerPlanetScr>().SetOldScale();
		}
		isToLarge = true;
	}

	public void SetOldScale()
	{
		isToLarge = false;
		isToNormal = true;
	}

	public void Update()
	{
		if (isToLarge) ToLarge();
		if (isToNormal) ToNormal();
	}

	private void ToLarge()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, largeScale, scaleSpeed * Time.deltaTime);
		if (transform.localScale.x >= largeScale.x)
		{
			isToLarge = false;
		}
	}

	private void ToNormal()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, oldScale, scaleSpeed * Time.deltaTime);
		if (transform.localScale.x <= oldScale.x)
		{
			isToNormal = false;
		}
	}


}

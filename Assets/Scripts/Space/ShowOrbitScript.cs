using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrbitScript : MonoBehaviour {
	Camera cam;
	LineRenderer lineRender;
	AnimationCurve curve;
	float orthographicSizeOld;

	void Start () {
		cam = GameObject.Find ("Camera").GetComponent<Camera>();
		lineRender =  gameObject.GetComponent<LineRenderer> ();
		curve = new AnimationCurve();
		curve.AddKey(0, 1);
		curve.AddKey(1, 1);
		orthographicSizeOld = 0;
		RedrawOrbit();
	}

	void Update () 
	{
		RedrawOrbit ();
	}

	void RedrawOrbit()
	{
		if (cam.orthographicSize == orthographicSizeOld) return;
		lineRender.widthCurve = curve;
		lineRender.startWidth = lineRender.endWidth = cam.orthographicSize / 1000 * Settings.LineOrbit.WIDTH;
		orthographicSizeOld = cam.orthographicSize;		
	}
}

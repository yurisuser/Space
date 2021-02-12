using System;
using UnityEngine;
using UnityEngine.EventSystems;
using settings = Settings.CameraStarSystem;
public class CameraStarSystem : MonoBehaviour
{
    private readonly string BackgroundObjectName = "BackgroundStarSystem";
    private Camera cam;
    private Vector3 position;
    private Vector3 mousePosition, mouseOldPosition, mouseZoomPosition;
    private float targetOrthographicSize;
    private Vector3 focusWorldCoordinates;
    private Ray ray;
    float SpeedCamMove
    {
        get
        {
            return (cam.orthographicSize * 0.002f);
        }
        set { }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = settings.DEFAULT_ZOOM;
        targetOrthographicSize = cam.orthographicSize;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Cursor.visible = true;
        position = transform.position;
        mousePosition = Input.mousePosition;
        MoveCamera();
        SetZoom();
        SmoothZoom();
    }

    void MoveCamera()
	{
        //if(!Input.GetMouseButton(0) && !Cursor.visible) Cursor.visible = true;
        if (Input.GetMouseButton(0))//&& RayCastFon())
		{
            //Cursor.visible = false;
            cam.transform.position += (mouseOldPosition - mousePosition) * SpeedCamMove;
            if (transform.position.x > settings.MAX_CAMERA_OFFSET)
            {
                position.x = settings.MAX_CAMERA_OFFSET;
                transform.position = position;
            }
            if (transform.position.y > settings.MAX_CAMERA_OFFSET)
            {
                position.y = settings.MAX_CAMERA_OFFSET;
                transform.position = position;
            }
            if (transform.position.x < settings.MAX_CAMERA_OFFSET * -1)
            {
                position.x = settings.MAX_CAMERA_OFFSET * -1;
                transform.position = position;
            }
            if (transform.position.y < settings.MAX_CAMERA_OFFSET * -1)
            {
                position.y = settings.MAX_CAMERA_OFFSET * -1;
                transform.position = position;
            }
        }
        mouseOldPosition = mousePosition;
	}

    bool RayCastFon()
	{
		ray = cam.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out RaycastHit hit)) return false;
		if (hit.collider.gameObject.name == BackgroundObjectName) return true;
		return false;
	}

	void SetZoom()	{
        if (Input.GetAxis("Mouse ScrollWheel") == 0 || !RayCastFon()) return;
        if (Input.GetAxis("Mouse ScrollWheel") > 0) targetOrthographicSize += settings.STEP_ZOOM * cam.orthographicSize / 10000;
        if (Input.GetAxis("Mouse ScrollWheel") < 0) targetOrthographicSize -= settings.STEP_ZOOM * cam.orthographicSize / 10000;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, settings.MIN_ZOOM, settings.MAX_ZOOM);
        focusWorldCoordinates = Utilities.ToWoldCoordinates(mousePosition, cam);
        mouseZoomPosition = mousePosition;
    }

    void SmoothZoom()
	{
		if (Math.Abs(cam.orthographicSize - targetOrthographicSize) < settings.STEP_SMOOTH) return;
		if (cam.orthographicSize < targetOrthographicSize)
			cam.orthographicSize += settings.STEP_SMOOTH * Mathf.Clamp(Math.Abs(cam.orthographicSize - targetOrthographicSize), 1, settings.MAX_DELTA_SMOOTH);
		if (cam.orthographicSize > targetOrthographicSize)
			cam.orthographicSize -= settings.STEP_SMOOTH * Mathf.Clamp(Math.Abs(cam.orthographicSize - targetOrthographicSize), 1, settings.MAX_DELTA_SMOOTH);
		cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, settings.MIN_ZOOM, settings.MAX_ZOOM);
		cam.transform.position += focusWorldCoordinates - Utilities.ToWoldCoordinates(mouseZoomPosition, cam);
	}
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using settings = Settings.CameraGalaxy;
public class CameraGalaxy : MonoBehaviour
{
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
			return (cam.orthographicSize * cam.aspect * 0.0020f);
		}
        set { }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        targetOrthographicSize = cam.orthographicSize = Glob.CameraGalaxyZoom;
        cam.transform.position = new Vector3(
            Galaxy.StarSystemsArr[Glob.sceneLocation.indexStarSystem].position.x,
            Galaxy.StarSystemsArr[Glob.sceneLocation.indexStarSystem].position.y,
            settings.CAMERA_LAYER
            );
    }
    void Update()
    {
        position = transform.position;
        mouseOldPosition = mousePosition;
        mousePosition = Input.mousePosition;
        MoveCamera();
        SetZoom();
        SmoothZoom();
    }

    void MoveCamera()
	{
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!Input.GetMouseButton(0) && !Cursor.visible) Cursor.visible = true;
        if (Input.GetMouseButton(0))
		{
			Cursor.visible = false;
			transform.position += (mouseOldPosition - mousePosition)  * SpeedCamMove;
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
	}

 //   bool RayCastFon(Camera cam , string objectName)
	//{
 //       RaycastHit hit;
	//	Ray ray = cam.ScreenPointToRay(Input.mousePosition);
 //       if (!Physics.Raycast(ray, out hit)) return false;
 //       if (EventSystem.current.IsPointerOverGameObject()) return false;
	//	if (hit.collider.gameObject.name == objectName) return true;
	//	return false;
	//}

	void SetZoom()	{
        if (Input.GetAxis("Mouse ScrollWheel") == 0) return;
        if (Input.GetAxis("Mouse ScrollWheel") > 0) targetOrthographicSize 
                += settings.STEP_ZOOM / (settings.MAX_ZOOM / (cam.orthographicSize + settings.STEP_ZOOM));
        if (Input.GetAxis("Mouse ScrollWheel") < 0) targetOrthographicSize 
                -= settings.STEP_ZOOM / (settings.MAX_ZOOM / (cam.orthographicSize + settings.STEP_ZOOM));
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, settings.MIN_ZOOM, settings.MAX_ZOOM);
        focusWorldCoordinates = Utilities.ToWoldCoordinates(mousePosition, cam);
        mouseZoomPosition = mousePosition;
        Glob.CameraGalaxyZoom = targetOrthographicSize;
    }

    void SmoothZoom()
	{
        if ( Math.Abs(cam.orthographicSize - targetOrthographicSize) < settings.STEP_SMOOTH) return;
        if (cam.orthographicSize < targetOrthographicSize)
            cam.orthographicSize += settings.STEP_SMOOTH * Mathf.Clamp(Math.Abs(cam.orthographicSize - targetOrthographicSize), 1, settings.MAX_DELTA_SMOOTH);
        if (cam.orthographicSize > targetOrthographicSize) 
            cam.orthographicSize -= settings.STEP_SMOOTH * Mathf.Clamp(Math.Abs(cam.orthographicSize - targetOrthographicSize), 1, settings.MAX_DELTA_SMOOTH);
		cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, settings.MIN_ZOOM, settings.MAX_ZOOM);
        cam.transform.position += focusWorldCoordinates - Utilities.ToWoldCoordinates(mouseZoomPosition, cam);
    }
}

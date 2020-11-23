using UnityEngine;

public class BackgroundGalaxy : MonoBehaviour
{
    public int height = 0;
    public int width = 0;
	private Camera cam;
    private Vector3 scale;
    private float camScale;
    private float ratio;
    private Vector3 newPosition;
    public Material paralaxMaterial;
    public float smallStarsDistance = 4000;
    public float mediumStarsDistance = 4000;
    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        ratio = gameObject.transform.localScale.x / gameObject.transform.localScale.y;
    }
    void LateUpdate()
	{
        Resize();
        FollowCamera();
        Paralax();
	}
    void Resize()
    {
        camScale = (cam.orthographicSize * 2) * cam.aspect;
        scale.z = 1;
        scale.x = camScale;
        scale.y = camScale / ratio;
        transform.localScale = scale;
    }

    void FollowCamera()
	{
        newPosition = cam.transform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
	}

    void Paralax()
	{
        paralaxMaterial.SetTextureOffset("_SmallStars", new Vector2(
            cam.transform.position.x / smallStarsDistance, 
            cam.transform.position.y / smallStarsDistance));
        paralaxMaterial.SetTextureOffset("_MediumStars", new Vector2(
            cam.transform.position.x / mediumStarsDistance, 
            cam.transform.position.y / mediumStarsDistance));
        paralaxMaterial.SetTextureScale("_MediumStars", new Vector2(
            cam.orthographicSize / Settings.CameraStarSystem.PARALAX_SCALE_FACTOR,
            cam.orthographicSize / Settings.CameraStarSystem.PARALAX_SCALE_FACTOR));
        paralaxMaterial.SetTextureScale("_SmallStars", new Vector2(
            cam.orthographicSize / Settings.CameraStarSystem.PARALAX_SCALE_FACTOR,
            cam.orthographicSize / Settings.CameraStarSystem.PARALAX_SCALE_FACTOR));
    }
}

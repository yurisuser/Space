using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPlanetScene : MonoBehaviour
{
    private int orbit = 0;
    private float Krotate = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orbit > 0)
		{
            Quaternion angle = new Quaternion(0f, 0f, 0f, 0f);
            transform.position = RotateAround(transform.position, Vector3.zero, Quaternion.Euler(0, 1f * Time.deltaTime, 0));
		}
        Rotate();
    }

    public void SetOrbit(int orbit)
	{
        this.orbit = orbit;
	}

    private Vector3 RotateAround(Vector3 point, Vector3 pivot, Quaternion angle)
	{
        return angle * (point - pivot) + pivot;
    }
    private void Rotate()
	{
        transform.Rotate(0, 0, (Krotate * orbit + Krotate) * Time.deltaTime);
	}
}

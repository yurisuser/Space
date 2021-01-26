using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPlanetScene : MonoBehaviour
{
    private int orbit = 0; // if 0 this is planet
    private float Krotate = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orbit > 0)
		{
            Quaternion angle = Quaternion.Euler(0, 0, 10f * Time.deltaTime);
            transform.position = RotateAround(transform.position, Vector3.zero, angle);
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
        if (orbit > 0)
		{
            transform.Rotate(0, 0, (Krotate * orbit + Krotate) * Time.deltaTime);
            return;
		}
        transform.Rotate(0, 0, -(Krotate * orbit + Krotate) * Time.deltaTime);
    }
}

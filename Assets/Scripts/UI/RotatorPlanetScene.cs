using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPlanetScene : MonoBehaviour
{
    private float rotateSelfSpeed = 2f;
    private float rotateOrbitSpeed = 10f;
    private int moonsCount = 0;
    private Moon moon;

    void Update()
    {
        if (moon != null)
		{
            var radius = (moon.orbitNumber + Settings.StarSystem.EMPTY_MOON_ORBIT) * Settings.StarSystem.RADIUS_MOON_ORBIT;
            Quaternion angle = Quaternion.Euler(0, 0, rotateOrbitSpeed / (moon.orbitNumber * 5 + 1) * Time.deltaTime);
			transform.position = RotateAround(transform.position, Vector3.zero, angle, radius);
		}
        Rotate();
    }

    public void SetMoon(Moon moon, int moonsCount)
	{
        this.moon = moon;
        this.moonsCount = moonsCount;
	}

	private Vector3 RotateAround(Vector3 point, Vector3 pivot, Quaternion angle, float radius)
	{
        return angle * (point - pivot).normalized * radius + pivot;
	}

	private void Rotate()
	{
        if (moon != null)
		{
            transform.Rotate(0, 0, (rotateSelfSpeed * moon.orbitNumber + rotateSelfSpeed) * Time.deltaTime);
            return;
		}
        transform.Rotate(0, 0, rotateSelfSpeed * Time.deltaTime);
    }
}

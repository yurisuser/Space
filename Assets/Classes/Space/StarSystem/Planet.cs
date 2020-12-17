using UnityEngine;

public class Planet: ISystemPosition
{
	public int id;
	public Star motherStar;
	public string name;
	public int type;
	public float mass;
	public int orbitNumber;
	public float orbitSpeed;
	public float rotateSpeed;
	public float angleOnOrbit;
	public ResourcePoint[] resources;
	public Vector3 position
	{
		get
		{
			Vector3 position = new Vector3(
				(orbitNumber + Settings.StarSystem.EMPTY_PLANET_ORBIT) * Settings.StarSystem.RADIUS_PLANET_ORBIT * Mathf.Cos(angleOnOrbit),
				(orbitNumber + Settings.StarSystem.EMPTY_PLANET_ORBIT) * Settings.StarSystem.RADIUS_PLANET_ORBIT * Mathf.Sin(angleOnOrbit),
				Settings.StarSystem.SYSTEM_SHIPS_LAYER);
			return position;
		}
		set { }
	}
}
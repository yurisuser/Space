using UnityEngine;

public class Moon: SubStarBody
{
	public Planet motherPlanet;

	public override Vector3 position
	{
		get
		{
			Vector3 position = new Vector3(
				(orbitNumber + Settings.StarSystem.EMPTY_MOON_ORBIT) * Settings.StarSystem.RADIUS_MOON_ORBIT * Mathf.Cos(angleOnOrbit) + motherPlanet.position.x,
				(orbitNumber + Settings.StarSystem.EMPTY_MOON_ORBIT) * Settings.StarSystem.RADIUS_MOON_ORBIT * Mathf.Sin(angleOnOrbit) + motherPlanet.position.y,
				Settings.StarSystem.SYSTEM_SHIPS_LAYER);
			return position;
		}
		set { }
	}

	public override void Tick()
	{
		throw new System.NotImplementedException();
	}
}
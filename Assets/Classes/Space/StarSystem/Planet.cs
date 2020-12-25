using UnityEngine;

public class Planet: SubStarBody
{
	public Star motherStar;

	public override Vector3 position
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

	public override void Tick()
	{
		throw new System.NotImplementedException();
	}
}
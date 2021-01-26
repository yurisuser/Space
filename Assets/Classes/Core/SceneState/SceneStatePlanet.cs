using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SceneStatePlanet : SceneState
{
	private StarSystem starSystem;
	private PlanetSystem planetSystem;
	private GameObject folder;
	public SceneStatePlanet(Location location)
	{
		starSystem = Galaxy.StarSystemsArr[location.indexStarSystem];
		planetSystem = starSystem.planetSystemsArray[location.indexPlanetSystem];
	}

	public override void DrawScene()
	{
		DrawSystem();
	}
	private void DrawSystem()
	{
		folder = new GameObject("sys");
		Planet planet = planetSystem.planet;
		GameObject go = GameObject.Instantiate(
			PrefabService.PlanetSystemMap[planet.type],
			Vector3.zero,
			Quaternion.identity);
		go.transform.localScale += new Vector3(
			planet.mass * Settings.StarSystem.PLANET_SCALE,
			planet.mass * Settings.StarSystem.PLANET_SCALE,
			planet.mass * Settings.StarSystem.PLANET_SCALE);
		go.transform.SetParent(folder.transform);
		go.AddComponent<RotatorPlanetScene>().SetOrbit(0);

		for (int i = 0; i <planetSystem.moonsArray.Length; i++)
		{
			 var moon = planetSystem.moonsArray[i];
			Vector3 moonposition = planet.position - planetSystem.moonsArray[i].position;
			moonposition.z = 0;
			GameObject obj = GameObject.Instantiate(PrefabService.PlanetSystemMap[moon.type], moonposition, Quaternion.identity);
			obj.name = planetSystem.planet.name + " " + (i + 1).ToString();
			obj.transform.localScale += new Vector3(
				moon.mass * Settings.StarSystem.MOON_SCALE,
				moon.mass * Settings.StarSystem.MOON_SCALE,
				moon.mass * Settings.StarSystem.MOON_SCALE);
			obj.transform.SetParent(folder.transform);
			obj.AddComponent<RotatorPlanetScene>().SetOrbit(i + 1);
		}

		folder.transform.Rotate(90f, 0, 0);
	}
}

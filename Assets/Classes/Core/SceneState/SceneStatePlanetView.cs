using UnityEngine;

class SceneStatePlanetView : SceneState
{
	private StarSystem starSystem;
	private PlanetSystem planetSystem;
	private GameObject folder;
	private Location location;
	public SceneStatePlanetView(Location location)
	{
		starSystem = Galaxy.StarSystemsArr[location.indexStarSystem];
		planetSystem = starSystem.planetSystemsArray[location.indexPlanetSystem];
		this.location = location;
	}

	public override void DrawScene()
	{
		HeadText(location);
		DrawSystem();
		DrawOrbits();
	}

	private void HeadText(Location location)
	{
		string str = null;
		switch (location.elocation)
		{
			case ELocation.planet:
				str = Galaxy.StarSystemsArr[location.indexStarSystem].planetSystemsArray[location.indexPlanetSystem].planet.name;
				break;
			case ELocation.moon:
				str = Galaxy.StarSystemsArr[location.indexStarSystem].planetSystemsArray[location.indexPlanetSystem].moonsArray[location.indexMoon].name;
				break;
			case ELocation.station:
				break;
			default:
				break;
		}
		GameObject.Find("LocationName").GetComponent<TMPro.TextMeshProUGUI>().text = str;
	}

	private void DrawSystem()
	{
		folder = new GameObject("sys");
		Planet planet = planetSystem.planet;
		GameObject go = GameObject.Instantiate(
			PrefabService.PlanetsView[planet.type],
			Vector3.zero,
			Quaternion.identity);
		go.transform.localScale += new Vector3(
			planet.mass * Settings.StarSystem.PLANET_SCALE,
			planet.mass * Settings.StarSystem.PLANET_SCALE,
			planet.mass * Settings.StarSystem.PLANET_SCALE);
		go.AddComponent<RotatorPlanetScene>().SetMoon(null, planetSystem.moonsArray.Length );

		for (int i = 0; i <planetSystem.moonsArray.Length; i++)
		{
			Moon moon = planetSystem.moonsArray[i];
			Vector3 moonposition = planet.position - planetSystem.moonsArray[i].position;
			moonposition.z = 0;
			GameObject obj = GameObject.Instantiate(PrefabService.PlanetsView[moon.type], moonposition, Quaternion.identity);
			obj.name = planetSystem.planet.name + " " + (i + 1).ToString();
			obj.transform.localScale += new Vector3(
				moon.mass * Settings.StarSystem.MOON_SCALE,
				moon.mass * Settings.StarSystem.MOON_SCALE,
				moon.mass * Settings.StarSystem.MOON_SCALE);
			obj.transform.SetParent(folder.transform);
			obj.AddComponent<RotatorPlanetScene>().SetMoon(moon, planetSystem.moonsArray.Length);
		}

		folder.transform.Rotate(0, 0, 90f);
	}

	private void DrawOrbits()
	{
		GameObject orbitFolder = new GameObject();
		float PointStep = (Mathf.PI * 2 / Settings.LineOrbit.CIRCLES_POINS);
		float ROrbit;
		for (int m = 0; m < planetSystem.moonsArray.Length; m++)
		{
			Moon moon = planetSystem.moonsArray[m];
			ROrbit = (moon.orbitNumber + Settings.LineOrbit.EMPTY_PLANET_ORBITS_COUNT) * Settings.LineOrbit.ORBIT_MOON_SIZE;
			Vector3[] vec3arr = new Vector3[Settings.LineOrbit.CIRCLES_POINS + 1];
			GameObject lineOrbit = new GameObject();
			for (int i = 0; i < Settings.LineOrbit.CIRCLES_POINS; i++)
			{
				vec3arr[i] = new Vector3((int)ROrbit * Mathf.Cos(i * PointStep), (int)ROrbit * Mathf.Sin(i * PointStep), 0);
			}
			vec3arr[vec3arr.Length - 1] = vec3arr[0];
			lineOrbit.name = "orbit moon" + moon.name;
			lineOrbit.AddComponent<LineRenderer>();
			LineRenderer lineRender = lineOrbit.GetComponent<LineRenderer>();
			lineRender.positionCount = Settings.LineOrbit.CIRCLES_POINS + 1;
			lineRender.SetPositions(vec3arr);
			lineRender.startColor = lineRender.endColor = Settings.LineOrbit.COLOR;
			lineRender.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
			lineRender.transform.SetParent(orbitFolder.transform);
			lineRender.startWidth = lineRender.endWidth = .25f;
		}
	}
}

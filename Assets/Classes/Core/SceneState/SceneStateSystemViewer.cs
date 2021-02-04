using System.Collections.Generic;
using UnityEngine;

public class SceneStateSystemViewer : SceneState
{
	private StarSystem starSystem;
	private PlanetSystem planetSystem;
	private GameObject folder;
	private Location location;
	private RectTransform parent;
	private List<GameObject> planetsSystemObjects = new List<GameObject>();

	private Vector2 starPosition = new Vector2(100, -120);

	public SceneStateSystemViewer(Location location)
	{
		starSystem = Galaxy.StarSystemsArr[location.indexStarSystem];
		planetSystem = starSystem.planetSystemsArray[location.indexPlanetSystem];
		this.location = location;
	}
	public override void DrawScene()
	{
		parent = GameObject.Find("Camera").transform.GetChild(0).GetComponent<RectTransform>();
		HeadText(location);
		DrawSystem();
	}

	private void HeadText(Location location)
	{
		string str = Galaxy.StarSystemsArr[location.indexStarSystem].star.name;
		GameObject.Find("StarName").GetComponent<TMPro.TextMeshProUGUI>().text = str;
	}

	private void DrawSystem()
	{
		folder = new GameObject("sys");
		DrawStar();
		DrawPlanets();
	}


	private Vector2 MagicCoordinate(Vector2 vec)
	{
		//смещение относительно центра канваса
		vec.x -= parent.rect.width * .5f;
		vec.y += parent.rect.height * .5f;
		return vec;
	}

	private void DrawStar()
	{
		GameObject star = GameObject.Instantiate(
			PrefabService.StarsSystemMap[Galaxy.StarSystemsArr[location.indexStarSystem].star.starClass],
			Vector3.zero,
			Quaternion.identity);
		var rt = star.AddComponent<RectTransform>();
		rt.anchorMin = new Vector2(0, 1);
		rt.anchorMax = new Vector2(0, 1);
		rt.pivot = new Vector2(0, 0);
		star.transform.SetParent(parent, true);
		star.transform.localScale = new Vector3(60, 60, 60);
		rt.localPosition = MagicCoordinate(starPosition);
	}

	private void DrawPlanets()
	{
		float x = 350;
		float y = -120;
		int step = 90;
		for (int i = 0; i < starSystem.planetSystemsArray.Length; i++)
		{
			Planet planet = starSystem.planetSystemsArray[i].planet;
			GameObject go = GameObject.Instantiate(
				PrefabService.PlanetSystemMap[planet.type],
				Vector3.zero,
				Quaternion.identity);
			var rt = go.AddComponent<RectTransform>();
			rt.anchorMin = new Vector2(0, 1);
			rt.anchorMax = new Vector2(0, 1);
			rt.pivot = new Vector2(0, 0);

			go.transform.SetParent(parent, true);
			rt.localPosition = MagicCoordinate(new Vector2(x + step * i, y));
			go.GetComponent<MeshRenderer>().receiveShadows = false;
			go.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			go.transform.localScale = new Vector3(30, 30, 30);

			GameObject.Destroy(go.GetComponent<PlanetSysMapScr>());
			var scr = go.AddComponent<SystemViewerPlanetScr>();
			scr.planet = planet;
			planetsSystemObjects.Add(go);
			scr.systemObjects = planetsSystemObjects;
			scr.oldScale = go.transform.localScale;
		}
	}
}

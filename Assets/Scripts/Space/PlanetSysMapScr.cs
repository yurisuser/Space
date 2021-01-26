using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetSysMapScr : MonoBehaviour
{
    public SubStarBody body;

    private GameObject panel;
    void Start()
    {

    }

    void Update()
    {
        
    }

	private void OnMouseDown()
	{
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Location loc;

        switch (body.subStarType)
		{
			case ESubStarType.planet:
                loc = new Location
                {
                    indexStarSystem = Glob.currentStarSystemIndex,
                    indexPlanetSystem = Array.FindIndex(Galaxy.StarSystemsArr[Glob.currentStarSystemIndex].planetSystemsArray, x => x.planet.id == body.id),
                    elocation = ELocation.planet
                };
                break;
			case ESubStarType.moon:
                int indexPlanetSystem = Array.FindIndex(Galaxy.StarSystemsArr[Glob.currentStarSystemIndex].planetSystemsArray, x => x.planet.id == body.parent.id);
                loc = new Location
                {
                    indexStarSystem = Glob.currentStarSystemIndex,
                    indexPlanetSystem = indexPlanetSystem,
                    indexMoon = Array.FindIndex(Galaxy.StarSystemsArr[Glob.currentStarSystemIndex].planetSystemsArray[indexPlanetSystem].moonsArray, x => x.id == body.id),
                    elocation = ELocation.moon
                };
                break;
			case ESubStarType.station:
                loc = new Location
                {
                    indexStarSystem = Glob.currentStarSystemIndex,
                    indexPlanetSystem = 0,
                    indexStation = Array.FindIndex(Galaxy.StarSystemsArr[Glob.currentStarSystemIndex].StationArr, x => x.id == body.id),
                    elocation = ELocation.station
                };
                break;
			default:
                throw new Exception();
		}
        Utilities.ShowMeObject(loc);
        Gmgr.gmgr.LoadScenePlanet(loc);
	}

    private void ShowPlanetPanel()
	{
        string goName = "PlanetPanel";
        if (GameObject.Find(goName) != null)
		{
            Destroy(GameObject.Find(goName));
            return;
		}
        panel = GameObject.Instantiate(
            PrefabService.UI.PlanetPanel,
            Vector3.zero,
            Quaternion.identity
            );
        panel.name = goName;
        panel.GetComponent<PlanetPanelScr>().body = body;
        UI.Escaper.Add(PlanetPanelDestroy);
	}

    public void PlanetPanelDestroy()
	{
        Destroy(panel);
	}
}

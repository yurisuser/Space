using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetSysMapScr : MonoBehaviour
{
    public SubStarBody body;

    private GameObject panel;
	private void OnMouseDown()
	{
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Location loc;

        switch (body.subStarType)
		{
			case ESubStarType.planet:
                loc = new Location
                {
                    indexStarSystem = Glob.sceneLocation.indexStarSystem,
                    indexPlanetSystem = Array.FindIndex(Galaxy.StarSystemsArr[Glob.sceneLocation.indexStarSystem].planetSystemsArray, x => x.planet.id == body.id),
                    elocation = ELocation.planet
                };
                break;
			case ESubStarType.moon:
                int indexPlanetSystem = Array.FindIndex(Galaxy.StarSystemsArr[Glob.sceneLocation.indexStarSystem].planetSystemsArray, x => x.planet.id == body.parent.id);
                loc = new Location
                {
                    indexStarSystem = Glob.sceneLocation.indexStarSystem,
                    indexPlanetSystem = indexPlanetSystem,
                    indexMoon = Array.FindIndex(Galaxy.StarSystemsArr[Glob.sceneLocation.indexStarSystem].planetSystemsArray[indexPlanetSystem].moonsArray, x => x.id == body.id),
                    elocation = ELocation.moon
                };
                break;
			case ESubStarType.station:
                loc = new Location
                {
                    indexStarSystem = Glob.sceneLocation.indexStarSystem,
                    indexPlanetSystem = 0,
                    indexStation = Array.FindIndex(Galaxy.StarSystemsArr[Glob.sceneLocation.indexStarSystem].StationArr, x => x.id == body.id),
                    elocation = ELocation.station
                };
                break;
			default:
                throw new Exception();
		}
        Utilities.ShowMeObject(loc);
        Gmgr.gmgr.LoadSceneSystemViever(loc);
	}

    public void PlanetPanelDestroy()
	{
        Destroy(panel);
	}
}

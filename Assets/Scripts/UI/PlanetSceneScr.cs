using UnityEngine;

class PlanetSceneScr : MonoBehaviour
{
	public TMPro.TextMeshProUGUI headLocation;
	public Location location;
	private void Start()
	{
	}

	public void SetLocation(Location loc)
	{
		this.location = loc;
		headLocation.text = $"{Galaxy.StarSystemsArr[Glob.currentStarSystemIndex].planetSystemsArray[Glob.currentPlanetSystemIndex].planet.name}";
		SetHeadText(loc);
	}
	private void Update()
	{
		
	}

	private void SetHeadText(Location loc)
	{
		switch (loc.elocation)
		{
			case ELocation.planet:
				headLocation.text = $"{Galaxy.StarSystemsArr[loc.indexStarSystem].planetSystemsArray[loc.indexPlanetSystem].planet.name}";
				break;
			case ELocation.moon:
				headLocation.text = $"{Galaxy.StarSystemsArr[loc.indexStarSystem].planetSystemsArray[loc.indexPlanetSystem].moonsArray[loc.indexMoon].name}";
				break;
			case ELocation.station:
				break;
			default:
				throw new System.Exception();
		}
	}
}

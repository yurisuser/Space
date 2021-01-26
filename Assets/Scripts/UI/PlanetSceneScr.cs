using UnityEngine;

class PlanetSceneScr : MonoBehaviour
{
	public TMPro.TextMeshProUGUI headLocation;
	public GameObject dd;
	private void Start()
	{
		headLocation.text = $"System: {Galaxy.StarSystemsArr[Glob.currentStarSystemIndex].star.name}" +
			$" planet: {Galaxy.StarSystemsArr[Glob.currentStarSystemIndex].planetSystemsArray[Glob.currentPlanetSystemIndex].planet.name}";
	}
	private void Update()
	{
		
	}
}

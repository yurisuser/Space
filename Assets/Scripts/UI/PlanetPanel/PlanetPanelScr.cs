using System;
using UnityEngine;
using UnityEngine.UI;

public class PlanetPanelScr : MonoBehaviour
{
    public Planet planet;

    private Text headtext;
    private string description;
    void Start()
    {
        headtext = transform.Find("Head").Find("HeadText").GetComponent<Text>();
        headtext.text = planet.name;
        ShowPlanetDescription();
    }


    private void ShowPlanetDescription()
	{
        transform.Find("inf").Find("PlanetDescription").GetComponent<Text>().text = 
        $"name: {planet.name}\n" +
        $"type : {Array.Find(Data.planetsArr, x => x.id == planet.type).type}\n" +
        $"mass (e.m.): {planet.mass} \n" +
        $"orbital speed: {planet.orbitSpeed}\n";

    }
    void Update()
    {

	}

    public void PlanetResources()
	{
        transform.Find("res").GetComponent<PreviewerBuilderScr>().BuildPreview(planet);
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class PlanetPanelScr : MonoBehaviour
{
    public Planet planet;

    private Text headtext;
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

    public void PlanetResources()
	{
        Transform planetPanel = transform.Find("res");
        planetPanel.GetComponent<PreviewerBuilderScr>().CreatePreview(planet, transform);
        transform.Find("res").Find("Text").GetComponent<Text>().text = $"available resource: {planetPanel.GetComponent<PreviewerBuilderScr>().GetCountResources().ToString()}";

    }
}

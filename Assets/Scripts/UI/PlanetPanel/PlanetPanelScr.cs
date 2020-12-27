using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlanetPanelScr : MonoBehaviour
{
    public SubStarBody body;

    private Text headtext;
    void Start()
    {
        headtext = transform.Find("Head").Find("HeadText").GetComponent<Text>();
        headtext.text = body.name;
        ShowPlanetDescription();
    }


    private void ShowPlanetDescription()
	{
        transform.Find("inf").Find("PlanetDescription").GetComponent<Text>().text =
        $"name: {body.name}\n" +
        $"resuorces fileds: {body.manufacture.industrialPointsArr.Length} type: {body.manufacture.industrialPointsArr.Select(x => x.resourceDeposit.idResource).Distinct().Count()}\n" +
        $"type : {Array.Find(Data.planetsArr, x => x.id == body.type).name}\n" +
        $"mass (e.m.): {body.mass} \n" +
        $"orbital speed: {body.orbitSpeed}\n";
    }

    public void PlanetResources()
	{
        Transform planetPanel = transform.Find("res");
        planetPanel.GetComponent<PreviewerBuilderScr>().CreatePreview(body, transform);
        transform.Find("res").Find("Text").GetComponent<Text>().text = $"available resource: {planetPanel.GetComponent<PreviewerBuilderScr>().GetCountResources()}";
    }
}

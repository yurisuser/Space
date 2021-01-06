using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetSysMapScr : MonoBehaviour
{
    public SubStarBody planet;

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
        Utilities.ShowMeObject(planet.storage.GetStorage());
        Utilities.ShowMeObject(planet.industry.stats);
        ShowPlanetPanel();
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
        panel.GetComponent<PlanetPanelScr>().body = planet;
        UI.Escaper.Add(PlanetPanelDestroy);
	}

    public void PlanetPanelDestroy()
	{
        Destroy(panel);
	}
}

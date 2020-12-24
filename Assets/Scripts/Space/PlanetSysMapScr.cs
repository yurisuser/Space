using UnityEngine;

public class PlanetSysMapScr : MonoBehaviour
{
    public SubStarBody planet;
    void Start()
    {

    }

    void Update()
    {
        
    }

	private void OnMouseDown()
	{
        Utilities.ShowMeObject(planet.storage.goodsArr);
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
        GameObject panel = GameObject.Instantiate(
            PrefabService.UI.PlanetPanel,
            Vector3.zero,
            Quaternion.identity
            );
        panel.name = goName;
        panel.GetComponent<PlanetPanelScr>().body = planet;
	}
}

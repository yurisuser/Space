using UnityEngine;
using UnityEngine.UI;

public class PreviewPlanetResourceScr : MonoBehaviour
{
    public int goodId;

    private Sprite img;
    void Start()
    {
        transform.Find("Panel").Find("Image").GetComponent<Image>().sprite = PrefabService.goodsImages[goodId];
	}

    void Update()
    {

    }
}

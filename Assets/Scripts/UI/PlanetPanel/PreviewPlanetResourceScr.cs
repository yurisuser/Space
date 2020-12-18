using UnityEngine;
using UnityEngine.UI;

public class PreviewPlanetResourceScr : MonoBehaviour
{
    private int goodsId;
    private int[] values = new int[]{0};

    void Start()
    {
        transform.Find("Panel").Find("Image").GetComponent<Image>().sprite = PrefabService.goodsImages[goodsId];
        transform.Find("ResourceName").GetComponent<Text>().text = Data.GetGoodsById(goodsId).name;
		transform.Find("Values").GetComponent<Text>().text = string.Join(", ", values);
	}

    public void SetParam(int goodsId, int[] values)
	{
        this.goodsId = goodsId;
        this.values = values;
	}
}

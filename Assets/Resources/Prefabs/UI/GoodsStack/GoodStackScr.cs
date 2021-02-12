using UnityEngine;
using UnityEngine.UI;

public class GoodStackScr : MonoBehaviour
{
	public Image image;
	public TMPro.TextMeshProUGUI goodsName;
	public TMPro.TextMeshProUGUI amaunt;

	public void SetStack(GoodsStack stack)
	{
		goodsName.text = Data.GetGoodsById(stack.id).name;
		this.amaunt.text = stack.amount.ToString();
		image.sprite = PrefabService.goodsImages[stack.id];
	}
}

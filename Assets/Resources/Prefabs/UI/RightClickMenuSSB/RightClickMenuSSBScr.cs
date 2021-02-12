using UnityEngine;

public class RightClickMenuSSBScr : MonoBehaviour
{
	public TMPro.TextMeshProUGUI header;

	private SubStarBody body;
	private string storageVieverName = "storageViewer";

	public void SetSubStarBody(SubStarBody body)
	{
		this.body = body;
		header.text = body.name;
	}

	public void SelectStorage()
	{

		GameObject canvas = GameObject.Find("Canvas");

		GameObject exist = GameObject.Find(storageVieverName);
		if (exist != null) Destroy(exist);

		var go = Instantiate(
			PrefabService.UI.StorageView,
			canvas.GetComponent<RectTransform>(),
			false
			);
		go.GetComponent<StorageViewScr>().AddContent(body.hub.storage);
		go.name = storageVieverName;
		Destroy(gameObject);
	}
}

using UnityEngine;

public class RightClickableObjectSSB : MonoBehaviour
{
	private SubStarBody ssb;
	private string menuName = "RCMenuSSB";
	private bool isMouseOver;

	private void RightClickHandler()
	{
		var exist = GameObject.Find(menuName);
		if (exist != null) Destroy(exist);

		GameObject canvas = GameObject.Find("Canvas");
		var menu = Instantiate(
			PrefabService.UI.RightClickMenuSSB,
			canvas.GetComponent<RectTransform>(),
			false);
		menu.name = menuName;
		var rt = menu.GetComponent<RectTransform>();
		rt.position = Input.mousePosition;
		menu.GetComponent<RightClickMenuSSBScr>().SetSubStarBody(ssb);
		Utilities.ShowMeObject(ssb.hub.storage);
	}

	public void SetSSB(SubStarBody ssb)
	{
		this.ssb = ssb;
	}

	private void Update()
	{
		if (isMouseOver && Input.GetMouseButtonDown(1))
		{
			RightClickHandler();
		}
	}

	private void OnMouseEnter()
	{
		isMouseOver = true;
	}

	private void OnMouseExit()
	{
		isMouseOver = false;
	}
}

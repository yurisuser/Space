using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Utilities
{
	private static string[] ShowMeArr;
	public static Vector3 ToWoldCoordinates(Vector3 position, Camera cam)
	{
		return cam.ScreenToWorldPoint(position);
	}
	public static Vector3 MouseAtCentreScreen()
	{
		return Input.mousePosition - new Vector3(Screen.width / 2 , Screen.height / 2, 0);
	}

	public static Vector3 ToScreenCoordinates(Vector3 position, Camera cam)
	{
		return cam.WorldToScreenPoint(position);
	}

	public static string NumberToLetter(int num)
	{
		return (num + 65).ToString();
	}

	public static GameObject WhoInRayCast(Camera cam)
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			return hit.transform.gameObject;
		}
		else
		{
			return cam.transform.gameObject;
		}
	}

	public static void ShowMe(int numstr, dynamic str)
	{
		const int size = 21;
		if (ShowMeArr == null) ShowMeArr = new string[size];
		ShowMeArr[numstr] = Convert.ToString(str);
		if (!GameObject.Find("ShowMePanel"))
		{
			Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
			GameObject ShowMePanel = GameObject.Instantiate(PrefabService.UI.ShowMePanel, Vector3.zero, Quaternion.identity);
			ShowMePanel.transform.SetParent(canvas.transform);
			ShowMePanel.name = "ShowMePanel";
			RectTransform rt = ShowMePanel.GetComponent<RectTransform>();
			rt.anchoredPosition = new Vector2(-rt.rect.width / 2, -rt.rect.height / 2);
		}
		for (int i = 0; i < size; i++)
		{
			GameObject.Find(("ShowMeText" + i.ToString()).ToString()).GetComponent<Text>().text = ShowMeArr[i];
		}
	}

	public static void ShowMeObject(object obj)
	{
		Debug.Log(JsonConvert.SerializeObject(obj));
	}
}

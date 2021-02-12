using System;
using System.Collections.Generic;
using UnityEngine;

public class StorageViewScr : MonoBehaviour
{
	public TMPro.TextMeshProUGUI header;
	public GameObject content;

	private List<GoodsStack> displayedGoods = new List<GoodsStack>();
	private List<GameObject> goStacks = new List<GameObject>();
	private Storage storage;
	private int lastTurn = 0;

	public void AddContent(Storage storage)
	{
		this.storage = storage;

		for (int i = 0; i < storage.goodsArr.Length; i++)
		{
			AddElement(storage.goodsArr[i]);
		}
	}

	private void AddElement(GoodsStack stack)
	{
		int index = displayedGoods.FindIndex(x => x.id == stack.id);
		if (index > -1) return;

		var go = Instantiate(
			PrefabService.UI.GoodsStack,
			content.GetComponent<RectTransform>(),
			false
			);
		go.GetComponent<GoodStackScr>().SetStack(stack);
		goStacks.Add(go);
		displayedGoods.Add(stack);
	}

	private void RemoveElement(int id)
	{
		var index = displayedGoods.FindIndex(x => x.id == id);
		if (index < 0) return;
		displayedGoods.RemoveAt(index);
		goStacks.RemoveAt(index);
	}

	private void UpdateElement(GoodsStack stack)
	{
		var index = displayedGoods.FindIndex(x => x.id == stack.id);
		if (index < 0) return;
		displayedGoods[index] = stack;
		goStacks[index].GetComponent<GoodStackScr>().SetStack(stack);
	}

	private void CheckUpdate()
	{
		for (int i = 0; i < storage.goodsArr.Length; i++)
		{
			int index = displayedGoods.FindIndex(x => x.id == storage.goodsArr[i].id);
			if (index < 0) {
				AddElement(storage.goodsArr[i]);
				continue;
			}
			UpdateElement(storage.goodsArr[i]);
		}

		for (int i = 0; i < displayedGoods.Count; i++)
		{
			int index = Array.FindIndex(storage.goodsArr, x => x.id == displayedGoods[i].id);
			if (index < 0) RemoveElement(displayedGoods[i].id);
		}
	}

	private void Update()
	{
		if (lastTurn < Turner.GetCurrentTime())
		{
			lastTurn = Turner.GetCurrentTime();
			CheckUpdate();
		}		
	}

	public void Close()
	{
		Destroy(gameObject);
	}
}

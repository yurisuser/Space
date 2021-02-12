using System;
using System.Collections.Generic;
using System.Linq;

public class Storage
{
	private SubStarBody parent;
	public GoodsStack[] goodsArr = new GoodsStack[0];

	public Storage(SubStarBody body)
	{
		parent = body;
	}

	public bool Add(GoodsStack stack)
	{
		if (stack.amount <= 0) return false;
		if (goodsArr == null) goodsArr = new GoodsStack[0];
		int index = Array.FindIndex(goodsArr, x => x.id == stack.id);
		if (index > -1)
		{
			goodsArr[index].amount += stack.amount;
			return true;
		}
		goodsArr = goodsArr.Concat(new GoodsStack[] { stack }).ToArray();
		return true;
	}

	public bool Subtract(GoodsStack stack)
	{
		if (goodsArr == null) 
			return false;
		int index = Array.FindIndex(goodsArr, x => x.id == stack.id);
		if (index < 0) 
			return false;
		if (goodsArr[index].amount < stack.amount) 
			return false;
		goodsArr[index].amount -= stack.amount;
		if (goodsArr[index].amount == 0) DeleteStack(index);
		return true;
	}

	public bool isEnoughGoods(GoodsStack stack)
	{
		return Array.Exists(goodsArr, x => x.id == stack.id && x.amount >= stack.amount);
	}


	public int GetGoodsAmount(int id)
	{
		return Array.Find(goodsArr, x => x.id == id).amount;
	}

	public GoodsStack[] GetStorage()
	{
		return goodsArr;
	}

	public void AddTestResources(SubStarBody body)
	{
		foreach (var construction in body.hub.industry.construction)
		{
			foreach (var item in construction.recipe.perTurn)
			{
				if (item.amount < 0)
				{
					Add(new GoodsStack
					{
						id = item.goodsId,
						amount = (int)(item.amount * Settings.Industry.PROCESSING_RESERV_TURN * -1)
					}); ;
				}
			}
		}
	}
	private void DeleteStack(int index)
	{
		List<GoodsStack> result = new List<GoodsStack>();
		for (int i = 0; i < goodsArr.Length; i++)
		{
			if (i == index) continue;
			result.Add(goodsArr[i]);
		}
		goodsArr = result.ToArray();
	}
}

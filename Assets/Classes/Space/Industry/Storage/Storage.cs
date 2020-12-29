using System;
using System.Collections.Generic;
using System.Linq;

public class Storage
{
	private SubStarBody parent;
	private GoodsStack[] goodsArr = new GoodsStack[0];

	public Storage(SubStarBody body)
	{
		parent = body;
	}

	public bool Add(GoodsStack stack)
	{
		if (stack.quantity <= 0) return false;
		if (goodsArr == null) goodsArr = new GoodsStack[0];
		int index = Array.FindIndex(goodsArr, x => x.id == stack.id);
		if (index > -1)
		{
			goodsArr[index].quantity += stack.quantity;
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
		if (goodsArr[index].quantity < stack.quantity) 
			return false;
		goodsArr[index].quantity -= stack.quantity;
		if (goodsArr[index].quantity == 0) DeleteStack(index);
		return true;
	}

	public bool isEnoughGoods(GoodsStack stack)
	{
		return Array.Exists(goodsArr, x => x.id == stack.id && x.quantity >= stack.quantity);
	}


	public int GetGoodsAmount(int id)
	{
		return (Array.Find(goodsArr, x => x.id == id)).quantity;
	}

	public GoodsStack[] GetStorage()
	{
		return goodsArr;
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

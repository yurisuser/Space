using System;
using System.Collections.Generic;
using System.Linq;

public struct Storage
{
	public GoodsStack[] goodsArr;

	public bool Add(GoodsStack stack)
	{
		if (stack.quantity <= 0) return false;
		if (goodsArr == null) goodsArr = new GoodsStack[0];
		int index = Array.FindIndex(goodsArr, x => x.id == stack.id);
		if (index >=  0)
		{
			goodsArr[index].quantity += stack.quantity;
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

	public bool isEnough(GoodsStack stack)
	{
		return Array.Exists(goodsArr, x => x.id == stack.id && x.quantity >= stack.quantity);
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

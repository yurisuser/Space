using System;
using System.Collections.Generic;
using System.Linq;

public class Storage
{
	private GoodsStack[] goodsArr;
	public readonly int limit;

	public Storage(int limit, GoodsStack[] goodsArr)
	{
		this.limit = limit;
		this.goodsArr = goodsArr;
	}

	public bool Add(GoodsStack stack)
	{
		if (stack.quantity <= 0) return false;
		if (!isEnoughSpace(stack.quantity)) return false;
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

	public bool isEnoughGoods(GoodsStack stack)
	{
		return Array.Exists(goodsArr, x => x.id == stack.id && x.quantity >= stack.quantity);
	}

	public bool isEnoughSpace(int newValue)
	{
		if (limit == 0) return true;
		int newSumm = goodsArr.Sum(x => x.quantity) + newValue;
		if (newSumm <= limit) return true;
		return false;
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

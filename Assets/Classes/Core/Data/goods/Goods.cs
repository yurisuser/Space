using System;

public partial struct Data
{
	public struct Goods
	{
		public int id;
		public string name;
		public int origin;
		public int price;
	}

	public static Goods GetGoodsById(int id)
	{
		int index = Array.FindIndex(goodsArr, x => x.id == id);
		if (index == -1) throw new Exception($"Cant find goods by ID: ({id}), Data");
		return goodsArr[index];
	}
}

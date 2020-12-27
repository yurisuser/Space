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
		return Array.Find(goodsArr, x => x.id == id);
	}
}

public static class StorageCreator
{
	public static Storage CreateStorage(GoodsStack[] initGoods, SubStarBody body)
	{
		Storage st = new Storage(body);
		if (initGoods != null)
		{
			for (int i = 0; i < initGoods.Length; i++)
			{
				st.Add(initGoods[i]);
			}
		}
		return st;
	}
}

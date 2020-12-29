public static class StorageCreator
{
	public static Storage CreateStorage(GoodsStack[] initGoods, SubStarBody body)
	{
		Storage storage = new Storage(body);
		if (initGoods != null)
		{
			for (int i = 0; i < initGoods.Length; i++)
			{
				storage.Add(initGoods[i]);
			}
		}
		return storage;
	}
}

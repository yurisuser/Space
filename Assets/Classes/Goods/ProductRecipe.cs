public partial struct Data
{
	public struct ProductRecipe
	{
		public int id;
		public string name;
		public string description;
		public int duration;
		public GoodsStack[] resources;
		public GoodsStack[] production;
	}

}

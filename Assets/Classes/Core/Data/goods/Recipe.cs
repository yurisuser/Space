public partial struct Data
{
	public class Recipe
	{
		public int id;
		public string name;
		public string description;
		public int duration;
		public GoodsStack[] resources;
		public GoodsStack[] production;
	}
}

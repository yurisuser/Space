public partial struct Data
{
	public class Recipe
	{
		public readonly int id;
		public readonly string name;
		public readonly string description;
		public readonly int duration;
		public readonly GoodsStack[] resources;
		public readonly GoodsStack[] production;

		public Recipe(int id, string name, string description, int duration, GoodsStack[] resources, GoodsStack[] production)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.duration = duration;
			this.resources = resources;
			this.production = production;
		}
	}
}

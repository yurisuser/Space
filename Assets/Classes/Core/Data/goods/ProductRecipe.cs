public partial struct Data
{
	public class ProductRecipe : Recipe
	{
		public ProductRecipe(int id, string name, string description, int duration, GoodsStack[] resources, GoodsStack[] production)
			: base(id, name, description, duration, resources, production) { }
	}
}

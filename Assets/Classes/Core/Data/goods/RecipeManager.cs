using System;

public partial struct Data
{
	public static class RecipeManager
	{
		public static Recipe GetRecipeByIdProduct(int id)
		{
			Recipe result = null;
			foreach (var recipe in productRecipeArr)
			{
				if (Array.Find(recipe.production, x => x.id == id).id == id) result = recipe;
			}
			if (result != null) return result;
			foreach (var recipe in miningRecipesArr)
			{
				if (Array.Find(recipe.production, x => x.id == id).id == id) result = recipe;
			}
			return result;

		}
	}
}

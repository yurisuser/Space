using System.Collections.Generic;

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
		public readonly PerTurn[] perTurn;

		public Recipe(int id, string name, string description, int duration, GoodsStack[] resources, GoodsStack[] production)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.duration = duration;
			this.resources = resources;
			this.production = production;
			this.perTurn = CalcPerTurn();
		}

		private PerTurn[] CalcPerTurn()
		{
			List<PerTurn> result = new List<PerTurn>();
			foreach (var item in resources)
			{
				result.Add(new PerTurn(item.id, item.quantity / (float)duration * -1));
			}
			return result.ToArray();
		}
	}

	public struct PerTurn
	{
		public readonly int goodsId;
		public readonly float amount;
		public PerTurn (int goodsId, float amount)
		{
			this.goodsId = goodsId;
			this.amount = amount;
		}
	}
}

using System;
using System.Collections.Generic;

public class IndustryStats
{
	public StatsGoodsElement[] elements;

	public void CalculateStats(Industry industry)
	{
		List<StatsGoodsElement> result = new List<StatsGoodsElement>();
		foreach (var construction in industry.construction)
		{
			foreach (var perTurn in construction.recipe.perTurn)
			{
				int index = result.FindIndex(x => x.goodsId == perTurn.goodsId);
				if (index > -1)
				{
					result[index] = new StatsGoodsElement
					{
						goodsId = result[index].goodsId,
						limitAmount = result[index].limitAmount + (int)(perTurn.amount * Settings.Industry.PROCESSING_RESERV_TURN)
					};
					continue;
				}
				result.Add(new StatsGoodsElement
				{
					goodsId = perTurn.goodsId,
					limitAmount = (int)(perTurn.amount * Settings.Industry.PROCESSING_RESERV_TURN)
				});
			}

		}
		elements = result.ToArray();
	}
}

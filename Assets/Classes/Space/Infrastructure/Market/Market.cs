using System;
using System.Collections.Generic;

public class Market
{
	private SubStarBody parent;

	public Market(SubStarBody subStarBody)
	{
		parent = subStarBody;
	}

	public GoodsOffer GetOffer(int goodsId)
	{
		var limit = Array.Find(parent.industry.stats.elements, x => x.goodsId == goodsId).limitAmount;
		if (limit == 0) return null;
		var amount = parent.storage.GetGoodsAmount(goodsId);
		return new GoodsOffer(
			goodsId: goodsId,
			limit: limit,
			amount: amount
			);
	}

	public GoodsOffer[] GetAllOffers()
	{
		GoodsStack[] arr = parent.storage.GetStorage();
		var limits = parent.industry.stats.elements;
		List<GoodsOffer> result = new List<GoodsOffer>();
		for (int i = 0; i < limits.Length; i++)
		{
			result.Add(new GoodsOffer(
				goodsId: limits[i].goodsId,
				limit: limits[i].limitAmount,
				amount: Array.Find(arr, x => x.id == limits[i].goodsId).quantity
				));
		}
		return result.ToArray();
	}
}

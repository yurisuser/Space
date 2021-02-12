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
		var limit = Array.Find(parent.hub.industry.stats.elements, x => x.goodsId == goodsId).limitAmount;
		if (limit == 0) return null;
		var amount = parent.hub.storage.GetGoodsAmount(goodsId);
		return new GoodsOffer(
			goodsId: goodsId,
			limit: limit,
			amount: amount
			);
	}

	public GoodsOffer[] GetAllOffers()
	{
		GoodsStack[] arr = parent.hub.storage.GetStorage();
		var limits = parent.hub.industry.stats.elements;
		List<GoodsOffer> result = new List<GoodsOffer>();
		for (int i = 0; i < limits.Length; i++)
		{
			result.Add(new GoodsOffer(
				goodsId: limits[i].goodsId,
				limit: limits[i].limitAmount,
				amount: Array.Find(arr, x => x.id == limits[i].goodsId).amount
				));
		}
		return result.ToArray();
	}
}

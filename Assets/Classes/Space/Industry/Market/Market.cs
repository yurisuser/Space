using System.Collections.Generic;

public class Market
{
	private SubStarBody parent;
	public List<GoodsOffer> sellOffers = new List<GoodsOffer>();
	public List<GoodsOffer> purchaseOffers = new List<GoodsOffer>();

	public Market(SubStarBody subStarBody)
	{
		parent = subStarBody;
		Init();
	}

	public void Init()
	{
		if (parent.industry == null || parent.industry.industrialPointsArr == null) return;
		InitOffers();
		RecalculateAllOffersPrices();
	}

	public void UpdateOffer(int goodsId)
	{
		int index = sellOffers.FindIndex(x => x.goodsId == goodsId);
		if (index < 0)
		{
			index = purchaseOffers.FindIndex(x => x.goodsId == goodsId);
			///////////////////////////////////////////////////////////////////////////////////////
		}
	}
	private void RecalculateAllOffersPrices()
	{
		for (int i = 0; i < sellOffers.Count; i++)
		{
			sellOffers[i].goodsPrice = CalculatePrice(sellOffers[i], true);
		}
		for (int i = 0; i < purchaseOffers.Count; i++)
		{
			purchaseOffers[i].goodsPrice = CalculatePrice(purchaseOffers[i], false);
		}
	}
	private void InitOffers()
	{
		//sellOffers = new List<GoodsOffer>();
		//purchaseOffers = new List<GoodsOffer>();
		//for (int i = 0; i < parent.industry.industrialPointsArr.Length; i++)
		//{
		//	for (int p = 0; p < parent.industry.industrialPointsArr[i].producingConstruction.recipe.production.Length; p++)
		//	{
		//		AddOffers(parent.industry.industrialPointsArr[i].producingConstruction.recipe.production,
		//			sellOffers,
		//			parent.industry.industrialPointsArr[i].producingConstruction.recipe.duration);

		//		AddOffers(parent.industry.industrialPointsArr[i].producingConstruction.recipe.resources,
		//			purchaseOffers,
		//			parent.industry.industrialPointsArr[i].producingConstruction.recipe.duration);
		//	}
		//}
	}

	private void AddOffers(GoodsStack[] recipeArr, List<GoodsOffer> resultList, int duration)
	{
		for (int i = 0; i < recipeArr.Length; i++)
		{
			GoodsOffer result;
			int index = resultList.FindIndex(x => x.goodsId == recipeArr[i].id);
			if (index < 0)
			{
				result = new GoodsOffer
				{
					goodsId = recipeArr[i].id,
					goodsQuantity = parent.storage.GetGoodsAmount(recipeArr[i].id),
					countPoints = 1,
					durationCycle = duration,
					cycleQuantity = recipeArr[i].quantity
				};
				resultList.Add(result);
				continue;
			}
			resultList[index].countPoints++;
		}
	}

	private int CalculatePrice(GoodsOffer offer, bool isSaleFlag)
	{
		int basePrice = Data.GetGoodsById(offer.goodsId).price;
		if (isSaleFlag)
		{
			return (int)(basePrice + basePrice * offer.normalizedPriceModificator() * Settings.Trade.PRICE_VOLATILE); 
		}
		return (int)(basePrice - basePrice * offer.normalizedPriceModificator() * Settings.Trade.PRICE_VOLATILE);
	}
}

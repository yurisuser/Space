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

	private void Init()
	{
		if (parent.industry == null || parent.industry.construction == null) return;
	}

	public void UpdateOffer(int goodsId)
	{
		int index = sellOffers.FindIndex(x => x.goodsId == goodsId);
		if (index < 0)
		{
			index = purchaseOffers.FindIndex(x => x.goodsId == goodsId);
		}
		//////////////////////////////////////////////
	}

	public void AddSellOffer(int goodsId)
	{
		int index = sellOffers.FindIndex(x => x.goodsId == goodsId);
		if (index > -1)
		{
			sellOffers[index].goodsAmount = parent.storage.GetGoodsAmount(goodsId);
			return;
		}
		sellOffers.Add(new GoodsOffer {
			goodsId = goodsId,
			goodsAmount = parent.storage.GetGoodsAmount(goodsId),
			goodsPrice = GetSellPrice(goodsId)
		});
	}

	public void AddPurchaseOffer(int goodsId)
	{

	}

	private int GetSellPrice(int goodsId)
	{
		return 1;
	}
}

public class GoodsOffer
{
	public int goodsId;
	public int goodsAmount;
	public int limit;
	public int basePrice
	{
		get => Data.GetGoodsById(goodsId).price;
	}

	public float fullness
	{
		get{
			float k = (float)goodsAmount / (float)limit;
			if (k > 1) k = 1;
			if (k < -1) k = -1;
			if (limit < 0) return k * 2 + 1;
			return k * 2 - 1;
			}
	}
	public int goodsPrice
	{
		get
		{
			float f = fullness;
			int mediumPrice = Data.GetGoodsById(goodsId).price;
			if (limit > 0)
				return (int)(mediumPrice - mediumPrice * (Settings.Trade.PRICE_VOLATILE * f));
			return (int)(mediumPrice + mediumPrice * (Settings.Trade.PRICE_VOLATILE * f));
		}
	}
	private GoodsOffer() { }
	public GoodsOffer(int goodsId, int amount, int limit)
	{
		this.goodsId = goodsId;
		this.goodsAmount = amount;
		this.limit = limit;
	}
}

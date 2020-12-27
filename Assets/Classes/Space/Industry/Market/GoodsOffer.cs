public class GoodsOffer
{
	public int goodsId;
	public int goodsPrice;
	public int goodsQuantity;
	public int countPoints;
	public int durationCycle;
	public int cycleQuantity;

	public float normalizedPriceModificator()
	{
		float k = goodsQuantity / (Settings.Trade.TURNS_STORAGE_RESERV * cycleQuantity / durationCycle);
		return k > 1 ? 1 : k;
	}
}

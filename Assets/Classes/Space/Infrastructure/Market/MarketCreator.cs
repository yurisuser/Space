public static class MarketCreator
{
	public static Market CreateMarket(SubStarBody body)
	{
		Market market = new Market(body);
		return market;
	}
}

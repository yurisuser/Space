public partial struct Data
{
	public enum EGoodsAggregation
	{
		Solid = 0,
		Liquid = 1,
		Gaseous = 2
	}

	public enum EGoodsSize
	{
		S = 0,
		M = 1,
		L = 2,
		XL = 3
	}

	public enum EGoodsOrigin
	{
		Unknow = 0,
		Planetary = 1
	}

	public enum EGoodsGroup
	{
		Unknow = 0,
		Metals = 1,
		Gases = 2,
		Nonmetals = 3,
		Organic = 4,
		Rare = 5
	}
}

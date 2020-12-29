public static class ResourcerCreator
{
	public static Resourcer CreateResourcer(ResourceDeposit[] resourceDeposits, SubStarBody body)
	{
		return new Resourcer(resourceDeposits, body);
	}
}

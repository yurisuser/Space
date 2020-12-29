public class Resourcer
{
	public ResourceDeposit[] resourceDeposits = null;

	private SubStarBody parent;

	public Resourcer(ResourceDeposit[] resourceDeposits, SubStarBody parent)
	{
		this.resourceDeposits = resourceDeposits == null ? new ResourceDeposit[0] : resourceDeposits;
		this.parent = parent;
	}

	public ResourceDeposit[] GetResources()
	{
		return resourceDeposits;
	}
}

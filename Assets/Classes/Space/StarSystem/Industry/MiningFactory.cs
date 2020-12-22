public class MiningFactory : IndustrialBuilding
{
	private ResourcePoint resourcePoint;
	private SubStarBody planet;

	public MiningFactory(ResourcePoint resourcePoint, SubStarBody planet)
	{
		this.resourcePoint = resourcePoint;
		this.planet = planet;
	}
	public override void Tick()
	{
		throw new System.NotImplementedException();
	}
}

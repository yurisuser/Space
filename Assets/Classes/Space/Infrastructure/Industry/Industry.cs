public class Industry
{
	private SubStarBody parent;

	public IndustryStats stats;
	public IndustryConstruction[] construction;

	public Industry(SubStarBody parent, IndustryConstruction[] construction)
	{
		this.construction = construction == null ? new IndustryConstruction[0] : construction;
		this.parent = parent;
		stats = new IndustryStats();
		stats.CalculateStats(this);
	}
}

public class Industry
{
	private SubStarBody parent;

	public IndustryStats stats;
	public IndustryConstruction[] construction;

	public Industry(SubStarBody parent)
	{
		this.parent = parent;
	}
}

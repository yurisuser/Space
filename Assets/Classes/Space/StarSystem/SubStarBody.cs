public abstract class SubStarBody
{
	public int id;
	public string name;
	public int type;
	public ESubStarType subStarType;
	public float mass;
	public int orbitNumber;
	public float orbitSpeed;
	public float rotateSpeed;
	public float angleOnOrbit;
	public IndustrialPoint[] industrialPointsArr;
	public GoodsStack[] storage;

	public abstract void Tick();
}


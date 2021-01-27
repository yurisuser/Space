public class Star
{
	public int id;
	public string name;
	public int indexSystem;
	public string starClass;
	public string colorName;
	public float temperature;
	public float mass;
	public float radius;
	public float luminosity;

	public Star(int id, string name, int indexSystem, string starClass, string colorName, float temperature, float mass, float radius, float luminosity)
	{
		this.id = id;
		this.name = name;
		this.indexSystem = indexSystem;
		this.starClass = starClass;
		this.colorName = colorName;
		this.temperature = temperature;
		this.mass = mass;
		this.radius = radius;
		this.luminosity = luminosity;
	}
}

using UnityEngine;

public struct StarSystem
{
	public int id;
	public float galaxyHandDistance;
	public Vector3 position;
	public Star star;
	public PlanetSystem[] planetSystemsArray;
	public Ship[] ShipsArr;
	public Station[] StationArr;
}

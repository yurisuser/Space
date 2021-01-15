using System.Collections.Generic;
using UnityEngine;

public class StarSystem
{
	public int id;
	public int indexSystem;
	public float galaxyHandDistance;
	public Vector3 position;
	public Star star;
	public PlanetSystem[] planetSystemsArray;
	public List<Ship> shipsList = new List<Ship>();
	public Station[] StationArr;
	public int idSector;
}

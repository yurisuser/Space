using UnityEngine;

public abstract class SubStarBody: ISystemPosition
{
	public int id;
	public int indexStarSystem;
	public string name;
	public int type;
	public ESubStarType subStarType;
	public float mass;
	public int orbitNumber;
	public float orbitSpeed;
	public float rotateSpeed;
	public float angleOnOrbit;
	public Resourcer resourcer;
	public Hub controlCentre;

	public SubStarBody()
	{
	}

	public virtual Vector3 position { 
		get => throw new System.NotImplementedException(); 
		set => throw new System.NotImplementedException(); 
	}

	public abstract void Tick();
}

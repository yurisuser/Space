﻿using UnityEngine;

public abstract class SubStarBody: ISystemPosition
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
	public Manufacture manufacture;
	public Storage storage;

	public SubStarBody()
	{
		storage = new Storage(0);
	}

	public virtual Vector3 position { 
		get => throw new System.NotImplementedException(); 
		set => throw new System.NotImplementedException(); 
	}

	public abstract void Tick();
}

using UnityEngine;

public struct Star
{
	public int id;
	public EStarTypes type;
	public string name;

	public Star(int id, EStarTypes type, string name)
	{
		this.id = id;
		this.type = type;
		this.name = name;
	}
}

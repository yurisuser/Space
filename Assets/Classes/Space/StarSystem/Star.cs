using UnityEngine;

public struct Star
{
	public int id;
	public int type;
	public string name;

	public Star(int id, int type, string name)
	{
		this.id = id;
		this.type = type;
		this.name = name;
	}
}

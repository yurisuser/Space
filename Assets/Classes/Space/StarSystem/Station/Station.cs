using UnityEngine;

public class Station : SubStarBody, ISystemPosition
{
	public Vector3 position 
	{
		get
		{
			return _position;
		}
		set
		{
			_position = value;
		}
	}

	private Vector3 _position;

	public override void Tick()
	{
		throw new System.NotImplementedException();
	}
}
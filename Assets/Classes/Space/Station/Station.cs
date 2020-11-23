using UnityEngine;

public class Station : ISystemPosition
{

	public StationParam param;
	public Vector3 position 
	{
		get
		{
			return param.currentPosition;
		}
		set {}
	}
}
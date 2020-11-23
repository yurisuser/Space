using UnityEngine;

public class Station : ISystemPosition
{
	public Data.StationParam param;
	public GoodsStack[] cargohold;
	public Vector3 position 
	{
		get
		{
			return param.currentPosition;
		}
		set {}
	}
}
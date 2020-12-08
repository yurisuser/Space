using UnityEngine;

public class Station : ISystemPosition
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
	public GoodsStack[] cargohold;
	public ProducingModule[] produceModuleArr;

	private Vector3 _position;
}
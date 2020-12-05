using UnityEngine;

public struct Station : ISystemPosition
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
	public ProduceModule[] produceModuleArr;

	private Vector3 _position;
}
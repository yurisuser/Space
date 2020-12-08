using UnityEngine;
using AI;

public class Ship : ISystemPosition
{
	public int Id;
	public string name;
	public Data.ShipRapam param;
	public GoodsStack cargohold;
	public Order order;
	public Vector3 position {
		get 
		{
			return order.attribute.currentPosition;
		}
		set { }
	}

	public float speed;

	public Vector3 dest;
}

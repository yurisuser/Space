using UnityEngine;
using AIShip;

public class Ship : ISystemPosition
{
	public bool isCalculateAI = false;
	public Data.ShipRapam param;
	public string name;
	public int Id;
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

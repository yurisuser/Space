using UnityEngine;
using AI;

public class Ship : ISystemPosition
{
	public int id;
	public string name;
	public Data.ShipRapam param;
	public GoodsStack cargohold;
	public Order order;
	public Dock homeBase;
	public EDockingState dockingState = EDockingState.undocked;
	public Vector3 position {
		get => order.attribute.currentPosition;
		set => order.attribute.currentPosition = value;
	}
	public Vector3 dest;
}

using UnityEngine;
using AI.AIShip;

public class Ship : ISystemPosition
{
	public int id;
	public string name;
	public Data.ShipRapam param;
	public GoodsStack cargohold;
	public Order order;
	public Dock homeBase;
	public Location location;
	public EShipState state = EShipState.inSpace;
	public Vector3 position {
		get => order.currentPosition;
		set => order.currentPosition = value;
	}
}

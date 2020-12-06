using AIShip;
using System.Collections.Generic;
using UnityEngine;

public class AIManager
{
	private static AIManager instance;
	private AIShip.AI aiShip;
	public static Dictionary<EOrders, ShipBehaviour> BehavioursShip = new Dictionary<EOrders, ShipBehaviour>();

	private AIManager() 
	{
		this.aiShip = AIShip.AI.getInstance();
	}

	public static AIManager getInstance()
	{
		if (instance == null) instance = new AIManager();
		return instance;
	}

	public void Tick()
	{
		aiShip.Tick();
	}
}

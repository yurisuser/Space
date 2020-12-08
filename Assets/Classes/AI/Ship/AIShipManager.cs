using AI;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	public class AIShipManager
	{
		private static AIShipManager instance;
		private AI.AIManager aiShip;
		public static Dictionary<EOrders, AIBehaviour<Ship>> BehavioursShip = new Dictionary<EOrders, AIBehaviour<Ship>>();

		private AIShipManager() 
		{
			this.aiShip = AI.AIManager.getInstance();
		}

		public static AIShipManager getInstance()
		{
			if (instance == null) instance = new AIShipManager();
			return instance;
		}

		public void Tick()
		{
			aiShip.Tick();
		}
	}
}

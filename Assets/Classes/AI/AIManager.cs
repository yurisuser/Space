using AI.AIShip;
using AI.AIStation;
using UnityEngine;

namespace AI
{
	public static class AIManager
	{
		public static void Tick()
		{
			AIShipManager.Tick();
			AIStationManager.Tick();
		}
	}
}

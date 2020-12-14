using AI.AIShip;
using AI.AIStation;

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

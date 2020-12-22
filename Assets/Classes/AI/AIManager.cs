using AI.AIShip;
using AI.AISubStar;

namespace AI
{
	public static class AIManager
	{
		public static void Tick()
		{
			AIShipManager.Tick();
			AISubStarManager.Tick();
		}
	}
}

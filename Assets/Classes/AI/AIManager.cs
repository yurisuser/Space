using AI.AIShip;
using AI.AISubStar;
using System.Threading;

namespace AI
{
	public static class AIManager
	{
		public static void Tick()
		{
			AIShipManager.Tick();
			//AISubStarManager.Tick();
		}
	}
}

using AI.AIShip;
using AI.AISubStar;
using System.Threading;

namespace AI
{
	public static class AIManager
	{
		public static void Tick()
		{
			AISubStarManager.Tick(); //должен быть первым. За время его работы gameObjects кораблей должны успеть покешировать Orders ИИ.
			AIShipManager.Tick();
		}
	}
}

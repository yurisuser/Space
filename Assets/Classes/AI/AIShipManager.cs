using System.Collections.Generic;

namespace AI.AIShip
{
	public static class AIShipManager
	{
		public static Dictionary<EOrders, AIBehaviour<Ship>> BehavioursShip = new Dictionary<EOrders, AIBehaviour<Ship>>()
		{
			{ EOrders.Idle, new BehavIdle() },
			{ EOrders.Patrol, new BehavePatrool() },
			{ EOrders.DockingTest, new BehaveDockingTest() },
			{ EOrders.JumpToSystem, new BehaveJumpToSystem() }
		};

		public static void Tick()
		{
			SelectShip();
		}

		private static void SelectShip()
		{
			for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
			{
				if (Galaxy.StarSystemsArr[i].shipsList == null) continue;
				for (int s = Galaxy.StarSystemsArr[i].shipsList.Count - 1; s >= 0; s--)
				{
					BehavioursShip[Galaxy.StarSystemsArr[i].shipsList[s].order.e_order].Tick(Galaxy.StarSystemsArr[i].shipsList[s]);
				}
			}
		}
	}
}

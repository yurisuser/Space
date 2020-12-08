using System.Collections.Generic;

namespace AI
{
	public class AIManager
	{
		private static AIManager instance;
		public static Dictionary<EOrders, AIBehaviour<Ship>> BehavioursShip = new Dictionary<EOrders, AIBehaviour<Ship>>();


		private AIManager()
		{
			CreateBehavioursShip();
		}
		public static AIManager getInstance()
		{
			if (instance == null) instance = new AIManager();
			return instance;
		}

		private void CreateBehavioursShip()
		{
			BehavioursShip.Add(EOrders.Idle, new BehavIdle());
			BehavioursShip.Add(EOrders.MoveToPosition, new BehavMoveToPosition());
			BehavioursShip.Add(EOrders.Patrol, new BehavPatrol());
		}

		public void Tick()
		{
			SelectShip();
		}

		private static void SelectShip()
		{
			for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
			{
				for (int s = 0; s < Galaxy.StarSystemsArr[i].ShipsArr.Length; s++)
				{
					ShipDo(Galaxy.StarSystemsArr[i].ShipsArr[s]);
				}
			}
		}

		private static void ShipDo(Ship ship)
		{
			BehavioursShip[ship.order.e_order].Tick(ship);
		}

	}
}

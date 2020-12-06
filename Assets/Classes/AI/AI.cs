using System.Collections.Generic;

namespace AIShip
{
	public class AI
	{
		private static AI instance;
		public static Dictionary<EOrders, ShipBehaviour> BehavioursShip = new Dictionary<EOrders, ShipBehaviour>();


		private AI()
		{
			CreateBehavioursShip();
		}
		public static AI getInstance()
		{
			if (instance == null) instance = new AI();
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

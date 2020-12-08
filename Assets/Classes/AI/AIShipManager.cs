﻿using System.Collections.Generic;

namespace AI
{
	public static class AIShipManager
	{
		public static Dictionary<EOrders, AIBehaviour<Ship>> BehavioursShip = new Dictionary<EOrders, AIBehaviour<Ship>>()
		{
			{ EOrders.Idle, new BehavIdle() },
			{ EOrders.MoveToPosition, new BehavMoveToPosition() },
			{ EOrders.Patrol, new BehavPatrol() }
		};

		public static void Tick()
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
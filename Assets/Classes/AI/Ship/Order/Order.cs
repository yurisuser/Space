using System.Collections.Generic;
using UnityEngine;

namespace AI.AIShip
{
	public class Order
	{
		public EOrders e_order;
		public Vector3 destinationOrder;
		public Vector3 destinationStep;
		public int destinationSystemIndex;
		public Vector3 currentPosition;
		public Queue<Vector3> wayPoints;
		public Dock dock;

		public Order Clone()
		{
			return new Order
			{
				e_order = e_order,
				destinationOrder = destinationOrder,
				destinationStep = destinationStep,
				destinationSystemIndex = destinationSystemIndex,
				currentPosition = currentPosition,
				wayPoints =  new Queue<Vector3>(wayPoints),
				dock = dock
			};
		}
	}
}
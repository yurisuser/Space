using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	public class OrderAttribute
	{
		public Vector3 destinationOrder;
		public Vector3 destinationStep;
		public Vector3 currentPosition;
		public Queue<Vector3> wayPoints;

		public OrderAttribute Clone()
		{
			return new OrderAttribute {
				destinationOrder = destinationOrder,
				destinationStep = destinationStep,
				currentPosition = currentPosition,
				wayPoints = wayPoints,
			};
		}
	}
}

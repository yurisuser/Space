namespace AI.AIShip
{
	class CreateNewOrder : Leaf<Ship>
	{
		private EOrders eOrder;

		public CreateNewOrder(EOrders eOrder)
		{
			this.eOrder = eOrder;
		}
		public override EStateNode Tick(Ship ship)
		{
			ship.order = OrderCreator.CreateOrder(eOrder, ship);
			return EStateNode.SUCCESS;
		}
	}
}

namespace AI.AIShip
{
	public class BehavePatrool: AIBehaviour<Ship>
	{
		public BehavePatrool()
		{
			behav = new Sequence<Ship>(
				new Selector<Ship>(
					new IsValidOrder(),
					new CreateNewOrder(EOrders.Patrol)
					),
				new NextStep()
				);
		}
	}
}

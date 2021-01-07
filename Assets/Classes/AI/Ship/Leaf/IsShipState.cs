namespace AI.AIShip
{
	public class IsShipState : Condition<Ship>
	{
		private EShipState state;
		public IsShipState(EShipState state)
		{
			this.state = state;
		}
		public override EStateNode Tick(Ship ship)
		{
			if (ship.state == state) return EStateNode.SUCCESS;
			return EStateNode.FAILURE;
		}
	}
}

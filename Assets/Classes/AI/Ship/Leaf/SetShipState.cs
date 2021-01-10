namespace AI.AIShip
{
	public class SetShipState : Leaf<Ship>
	{
		private EShipState state;
		public SetShipState(EShipState state)
		{
			this.state = state;
		}
		public override EStateNode Tick(Ship ship)
		{
			ship.state = state;
			return EStateNode.SUCCESS;
		}
	}
}

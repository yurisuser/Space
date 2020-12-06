using AI;

namespace AIShip
{
	public abstract class ShipBehaviour : Node
	{
		protected Node behav;
		public override EStateNode Tick(Ship ship)
		{
			return behav.Tick(ship);
		}
	}
}
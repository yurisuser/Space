namespace AIShip
{
	public class BehavIdle : ShipBehaviour
	{
		public BehavIdle()
		{
			
		}
		public override EStateNode Tick(Ship ship)
		{
			return EStateNode.SUCCESS;
		}
	}
}
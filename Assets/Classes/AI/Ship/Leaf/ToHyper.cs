namespace AI.AIShip
{
	public class ToHyper : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			Docker.ToHyper(ship);
			return EStateNode.SUCCESS;
		}
	}
}

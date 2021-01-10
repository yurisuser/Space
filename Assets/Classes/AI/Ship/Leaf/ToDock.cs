namespace AI.AIShip
{
	class ToDock : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			Docker.ToDock(ship);
			return EStateNode.SUCCESS;
		}
	}
}

namespace AI
{
	public class AlwaysState : Node
	{
		private EStateNode state;
		public AlwaysState(Node node, EStateNode state) : base(node)
		{
			this.state = state;
		}
		public override EStateNode Tick(Ship ship)
		{
			nodes[0].Tick(ship);
			return state;
		}
	}
}
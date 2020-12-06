namespace AI
{
	public class Repeater : Node
	{
		public Repeater(Node node): base (node) {}

		public override EStateNode Tick(Ship ship)
		{
			for (int i = 0; i < Settings.AI.REPEATING_CICLE; i++)
			{
				if (nodes[0].Tick(ship) == EStateNode.FAILURE) return EStateNode.FAILURE;
			}
			return EStateNode.FAILURE;
		}
	}
}
namespace AI
{
	public class Invertor : Node
	{
		public Invertor(Node node): base (node) {}

		public override EStateNode Tick(Ship ship)
		{
			EStateNode result = nodes[0].Tick(ship);
			if (result == EStateNode.FAILURE) return EStateNode.SUCCESS;
			if (result == EStateNode.SUCCESS) return EStateNode.FAILURE;
			return result;
		}
	}
}
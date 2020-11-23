namespace AIShip
{
	public class Sequence : Node
	{
		//Последовательность. Работает до первого не SUCCESS
		public Sequence(params Node[] nodeArr) : base(nodeArr) { }

		public override EStateNode Tick(Ship ship)
		{
			EStateNode result;
			foreach (var item in nodes)
			{
				result = item.Tick(ship);
				if (result != EStateNode.SUCCESS) return result;
			}
			return EStateNode.SUCCESS;
		}
	}
}
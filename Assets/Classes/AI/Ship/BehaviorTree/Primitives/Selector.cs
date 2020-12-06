namespace AI
{
	public class Selector: Node
	{
		//Селектор. Работает до первого не FAILURE
		public Selector(params Node[] nodeArr) : base(nodeArr) { }

		public override EStateNode Tick(Ship ship)
		{
			EStateNode result;
			foreach (var item in nodes)
			{
				result = item.Tick(ship);
				if (result != EStateNode.FAILURE) return result;
			}
			return EStateNode.FAILURE;
		}
	}
}
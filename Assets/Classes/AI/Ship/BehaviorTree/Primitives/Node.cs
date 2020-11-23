namespace AIShip
{
	public abstract class Node
	{
		protected Node[] nodes;

		public Node(params Node[] nodeArr)
		{
			nodes = nodeArr;
		}
		private Node() { }
		public abstract EStateNode Tick(Ship ship);
	}
}
namespace AI
{
	public abstract class Node<T>
	{
		protected Node<T>[] nodes;

		public Node(params Node<T>[] nodeArr)
		{
			nodes = nodeArr;
		}
		private Node() { }
		public abstract EStateNode Tick(T subj);
	}
}
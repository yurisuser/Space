namespace AI
{
	public abstract class Node<T>
	{
		private string name;
		protected Node<T>[] nodes;

		public Node(string name, params Node<T>[] nodeArr)
		{
			this.name = name;
			nodes = nodeArr;
		}

		public Node(params Node<T>[] nodeArr)
		{
			nodes = nodeArr;
		}
		private Node() { }
		public abstract EStateNode Tick(T subj);
	}
}
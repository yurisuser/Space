namespace AI
{
	public class Sequence<T> : Node<T>
	{
		//Последовательность. Работает до первого FAILURE
		public Sequence(params Node<T>[] nodeArr) : base(nodeArr) { }
		public Sequence(string name, params Node<T>[] nodeArr) : base(name, nodeArr) { }

		public override EStateNode Tick(T subj)
		{
			EStateNode result;
			foreach (var item in nodes)
			{
				result = item.Tick(subj);
				if (result == EStateNode.FAILURE) return result;
			}
			return EStateNode.SUCCESS;
		}
	}
}
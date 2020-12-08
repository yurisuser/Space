namespace AI
{
	public class Sequence<T> : Node<T>
	{
		//Последовательность. Работает до первого не SUCCESS
		public Sequence(params Node<T>[] nodeArr) : base(nodeArr) { }

		public override EStateNode Tick(T subj)
		{
			EStateNode result;
			foreach (var item in nodes)
			{
				result = item.Tick(subj);
				if (result != EStateNode.SUCCESS) return result;
			}
			return EStateNode.SUCCESS;
		}
	}
}
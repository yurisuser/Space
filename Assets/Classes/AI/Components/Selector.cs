namespace AI
{
	public class Selector<T>  : Node<T>
	{
		//Селектор. Работает до первого SUCCESS
		public Selector(params Node<T>[] nodeArr) : base(nodeArr) { }
		public Selector(string name, params Node<T>[] nodeArr) : base(name, nodeArr) { }

		public override EStateNode Tick(T subj)
		{
			EStateNode result;
			foreach (var item in nodes)
			{
				result = item.Tick(subj);
				if (result == EStateNode.SUCCESS) return result;
			}
			return EStateNode.FAILURE;
		}
	}
}
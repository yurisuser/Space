namespace AI
{
	public class Selector<T>  : Node<T>
	{
		//Селектор. Работает до первого не FAILURE
		public Selector(params Node<T>[] nodeArr) : base(nodeArr) { }

		public override EStateNode Tick(T subj)
		{
			EStateNode result;
			foreach (var item in nodes)
			{
				result = item.Tick(subj);
				if (result != EStateNode.FAILURE) return result;
			}
			return EStateNode.FAILURE;
		}
	}
}
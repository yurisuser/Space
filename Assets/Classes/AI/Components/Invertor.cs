namespace AI
{
	public class Invertor<T> : Node<T>
	{
		public Invertor(Node<T> node): base (node) {}

		public override EStateNode Tick(T subj)
		{
			EStateNode result = nodes[0].Tick(subj);
			if (result == EStateNode.FAILURE) return EStateNode.SUCCESS;
			if (result == EStateNode.SUCCESS) return EStateNode.FAILURE;
			return result;
		}
	}
}
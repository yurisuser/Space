namespace AI
{
	public class Repeater<T> : Node<T>
	{
		public Repeater(Node<T> node): base (node) {}

		public override EStateNode Tick(T subj)
		{
			for (int i = 0; i < Settings.AI.REPEATING_CICLE; i++)
			{
				if (nodes[0].Tick(subj) == EStateNode.FAILURE) return EStateNode.FAILURE;
			}
			return EStateNode.FAILURE;
		}
	}
}
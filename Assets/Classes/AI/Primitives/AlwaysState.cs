namespace AI
{
	public class AlwaysState<T> : Node<T>
	{
		private EStateNode state;
		public AlwaysState(Node<T> node, EStateNode state) : base(node)
		{
			this.state = state;
		}
		public override EStateNode Tick(T subj)
		{
			nodes[0].Tick(subj);
			return state;
		}
	}
}
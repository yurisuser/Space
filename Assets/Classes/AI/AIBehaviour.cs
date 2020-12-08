namespace AI
{
	public abstract class AIBehaviour<T> : Node<T>
	{
		protected Node<T> behav;
		public override EStateNode Tick(T ship)
		{
			return behav.Tick(ship);
		}
	}
}
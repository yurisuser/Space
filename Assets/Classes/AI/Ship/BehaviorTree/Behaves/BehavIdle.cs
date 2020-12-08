using AI;

namespace AI
{
	public class BehavIdle : AIBehaviour<Ship>
	{
		public BehavIdle()
		{
			
		}
		public override EStateNode Tick(Ship ship)
		{
			return EStateNode.SUCCESS;
		}
	}
}
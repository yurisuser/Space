using AI;

namespace AI
{
	public class BehavPatrol : AIBehaviour<Ship>
	{
		public BehavPatrol()
		{
			behav =	new Repeater<Ship>(
				new Sequence<Ship>(
					new StartStepInit(),
					new Invertor<Ship>(new BehavMoveToPosition()),
					new GetRandomPosition()
					)
				);
		}
	}
}
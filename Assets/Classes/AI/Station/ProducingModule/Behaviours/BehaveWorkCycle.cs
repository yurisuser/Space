using System;

namespace AI.AIStation.ProdModule
{
	public class WorkCycle : AIBehaviour<ProdModuleWrapper>
	{
		public WorkCycle()
		{
			Sequence<ProdModuleWrapper> startCycle = new Sequence<ProdModuleWrapper>(
				
				);

			//behav = 
			//	new Sequence<ProdModuleWrapper>(
			//		new Invertor<ProdModuleWrapper>(new IsModuleState(EProducingState.pause)),
			//		new IsModuleState(EProducingState.work),
			//		new Sequence<ProdModuleWrapper>(
			//			new IsNotLastStepProcess(),
			//			new NextWorkCycle()
			//			)
			//		);

		}

		public override EStateNode Tick(ProdModuleWrapper subj)
		{
			throw new NotImplementedException();
		}
	}
}

using System;

namespace AI.AIStation.ProdModule
{
	public class WorkCycle : AIBehaviour<ProdModuleWrapper>
	{
		public WorkCycle()
		{
			Sequence<ProdModuleWrapper> checkDeficit = new Sequence<ProdModuleWrapper>(
				new IsDeficitResources(),
				new SetModuleState(EProducingState.deficitRecorces)
				);


			Sequence<ProdModuleWrapper> lastStep = new Sequence<ProdModuleWrapper>(
					new IsLastStepOfProcess(),
					new FinishWork(),
					new SetModuleState(EProducingState.finished)
					);

			Sequence<ProdModuleWrapper> workChain = new Sequence<ProdModuleWrapper>(
				new IsModuleState(EProducingState.work),
				new NextWorkCycle(),
				lastStep
				);


			Sequence<ProdModuleWrapper> startProcess = new Sequence<ProdModuleWrapper>(
				new IsModuleState(EProducingState.finished),
				new Invertor<ProdModuleWrapper>(checkDeficit),
				new StartNewProcess(),
				new SetModuleState(EProducingState.work),
				lastStep
				);

			behav = new Selector<ProdModuleWrapper>(
				new IsModuleState(EProducingState.pause),
				workChain,
				startProcess
				);
		}

		public override EStateNode Tick(ProdModuleWrapper subj)
		{
			throw new NotImplementedException();
		}
	}
}

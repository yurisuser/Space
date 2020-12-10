using System;

namespace AI.AIStation.ProdModule
{
	public class BehaveProducingModule : AIBehaviour<ProdModuleWrapper>
	{
		public BehaveProducingModule()
		{
			Sequence<ProdModuleWrapper> checkDeficit = new Sequence<ProdModuleWrapper>(
				"checkDeficit",
				new IsDeficitResources(),
				new SetModuleState(EProducingState.deficitRecorces)
				);


			Sequence<ProdModuleWrapper> lastStep = new Sequence<ProdModuleWrapper>(
				"lastStep",
				new IsLastStepOfProcess(),
				new FinishWork(),
				new SetModuleState(EProducingState.finished)
				);

			Sequence<ProdModuleWrapper> workChain = new Sequence<ProdModuleWrapper>(
				"workChain",
				new IsModuleState(EProducingState.work),
				new NextWorkCycle(),
				lastStep
				);


			Sequence<ProdModuleWrapper> startProcess = new Sequence<ProdModuleWrapper>(
				"startProcess",
				new IsModuleState(EProducingState.finished),
				new Invertor<ProdModuleWrapper>(checkDeficit),
				new StartNewProcess(),
				new SetModuleState(EProducingState.work),
				lastStep
				);

			behav = new Selector<ProdModuleWrapper>(
				"behav",
				new IsModuleState(EProducingState.pause),
				workChain,
				startProcess
				);
		}

		public override EStateNode Tick(ProdModuleWrapper w)
		{
			if (Turner.GetCurrentTime() > 3)
			{
				var i = 2;
			}
			return behav.Tick(w);
		}
	}
}

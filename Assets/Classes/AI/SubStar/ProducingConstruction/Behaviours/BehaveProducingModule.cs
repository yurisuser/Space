using System;

namespace AI.AISubStar.ProdModule
{
	public class BehaveProducingModule : AIBehaviour<ProdConstructionWrapper>
	{
		public BehaveProducingModule()
		{
			Sequence<ProdConstructionWrapper> checkDeficit = new Sequence<ProdConstructionWrapper>(
				"checkDeficit",
				new IsDeficitResources(),
				new SetModuleState(EProducingState.deficitRecorces)
				);


			Sequence<ProdConstructionWrapper> lastStep = new Sequence<ProdConstructionWrapper>(
				"lastStep",
				new IsLastStepOfProcess(),
				new FinishWork(),
				new SetModuleState(EProducingState.finished)
				);

			Sequence<ProdConstructionWrapper> workChain = new Sequence<ProdConstructionWrapper>(
				"workChain",
				new IsModuleState(EProducingState.work),
				new NextWorkCycle(),
				lastStep
				);


			Sequence<ProdConstructionWrapper> startProcess = new Sequence<ProdConstructionWrapper>(
				"startProcess",
				new IsModuleState(EProducingState.finished),
				new Invertor<ProdConstructionWrapper>(checkDeficit),
				new StartNewProcess(),
				new SetModuleState(EProducingState.work),
				lastStep
				);

			behav = new Selector<ProdConstructionWrapper>(
				"behav",
				new IsModuleState(EProducingState.pause),
				workChain,
				startProcess
				);
		}

		public override EStateNode Tick(ProdConstructionWrapper w)
		{
			return behav.Tick(w);
		}
	}
}

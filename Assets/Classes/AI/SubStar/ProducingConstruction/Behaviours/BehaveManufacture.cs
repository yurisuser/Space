using System;

namespace AI.AISubStar.Manufacture
{
	public class BehaveManufacture : AIBehaviour<ManufactureWrapper>
	{
		public BehaveManufacture()
		{
			Sequence<ManufactureWrapper> checkDeficit = new Sequence<ManufactureWrapper>(
				"checkDeficit",
				new IsDeficitResources(),
				new SetModuleState(EProducingState.deficitRecorces)
				);


			Sequence<ManufactureWrapper> lastStep = new Sequence<ManufactureWrapper>(
				"lastStep",
				new IsLastStepOfProcess(),
				new FinishWork(),
				new SetModuleState(EProducingState.finished)
				);

			Sequence<ManufactureWrapper> workChain = new Sequence<ManufactureWrapper>(
				"workChain",
				new IsModuleState(EProducingState.work),
				new NextWorkCycle(),
				lastStep
				);


			Sequence<ManufactureWrapper> startProcess = new Sequence<ManufactureWrapper>(
				"startProcess",
				new IsModuleState(EProducingState.finished),
				new Invertor<ManufactureWrapper>(checkDeficit),
				new StartNewProcess(),
				new SetModuleState(EProducingState.work),
				lastStep
				);

			behav = new Selector<ManufactureWrapper>(
				"behav",
				new IsModuleState(EProducingState.pause),
				workChain,
				startProcess
				);
		}

		public override EStateNode Tick(ManufactureWrapper w)
		{
			return behav.Tick(w);
		}
	}
}

namespace AI.AISubStar.Manufacture
{
	public class SetModuleState : Leaf<ManufactureWrapper>
	{
		private EProducingState state;
		private SetModuleState() { }

		public SetModuleState(EProducingState state)
		{
			this.state = state;
		}
		public override EStateNode Tick(ManufactureWrapper w)
		{
			w.module.state = state;
			return EStateNode.SUCCESS;
		}
	}
}

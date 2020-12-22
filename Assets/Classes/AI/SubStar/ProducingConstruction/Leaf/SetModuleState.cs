namespace AI.AISubStar.ProdModule
{
	public class SetModuleState : Leaf<ProdConstructionWrapper>
	{
		private EProducingState state;
		private SetModuleState() { }

		public SetModuleState(EProducingState state)
		{
			this.state = state;
		}
		public override EStateNode Tick(ProdConstructionWrapper w)
		{
			w.module.state = state;
			return EStateNode.SUCCESS;
		}
	}
}

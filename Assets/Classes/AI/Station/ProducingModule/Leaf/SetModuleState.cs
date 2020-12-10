namespace AI.AIStation.ProdModule
{
	public class SetModuleState : Leaf<ProdModuleWrapper>
	{
		private EProducingState state;
		private SetModuleState() { }

		public SetModuleState(EProducingState state)
		{
			this.state = state;
		}
		public override EStateNode Tick(ProdModuleWrapper w)
		{
			w.module.state = state;
			return EStateNode.SUCCESS;
		}
	}
}

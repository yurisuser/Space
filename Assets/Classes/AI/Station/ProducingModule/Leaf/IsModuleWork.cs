namespace AI.AIStation.ProdModule
{
	public class IsModuleWork : Condition<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper w)
		{
			if (w.module.state == EProducingState.work) return EStateNode.SUCCESS;
			return EStateNode.FAILURE;
		}
	}
}

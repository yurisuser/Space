namespace AI.AIStation.ProdModule
{
	public class IsModuleNotPaused : Condition<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper w)
		{
			if (w.module.state == EProducingState.pause) return EStateNode.FAILURE;
			return EStateNode.SUCCESS;
		}
	}
}

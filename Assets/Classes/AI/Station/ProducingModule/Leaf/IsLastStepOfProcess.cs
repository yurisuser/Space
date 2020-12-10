namespace AI.AIStation.ProdModule
{
	public class IsLastStepOfProcess : Condition<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper w)
		{
			if (w.module.stageProcess >= w.module.recipe.duration)
			{
				return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}

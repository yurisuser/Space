namespace AI.AISubStar.ProdModule
{
	public class IsLastStepOfProcess : Condition<ProdConstructionWrapper>
	{
		public override EStateNode Tick(ProdConstructionWrapper w)
		{
			if (w.module.stageProcess >= w.module.recipe.duration)
			{
				return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}

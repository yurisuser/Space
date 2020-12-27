namespace AI.AISubStar.Manufacture
{
	public class IsLastStepOfProcess : Condition<ManufactureWrapper>
	{
		public override EStateNode Tick(ManufactureWrapper w)
		{
			if (w.module.stageProcess >= w.module.recipe.duration)
			{
				return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}

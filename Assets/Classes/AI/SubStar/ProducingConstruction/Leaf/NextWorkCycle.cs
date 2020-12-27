namespace AI.AISubStar.Manufacture
{
	public class NextWorkCycle : Node<ManufactureWrapper>
	{
		public override EStateNode Tick(ManufactureWrapper w)
		{
			w.module.stageProcess++;
			return EStateNode.SUCCESS;
		}
	}
}

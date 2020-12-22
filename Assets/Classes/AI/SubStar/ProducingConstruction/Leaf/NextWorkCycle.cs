namespace AI.AISubStar.ProdModule
{
	public class NextWorkCycle : Node<ProdConstructionWrapper>
	{
		public override EStateNode Tick(ProdConstructionWrapper w)
		{
			w.module.stageProcess++;
			return EStateNode.SUCCESS;
		}
	}
}

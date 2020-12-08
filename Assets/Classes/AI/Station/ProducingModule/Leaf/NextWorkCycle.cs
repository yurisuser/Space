namespace AI.AIStation.ProdModule
{
	public class NextWorkCycle : Node<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper w)
		{
			w.module.stageProcess++;
			return EStateNode.SUCCESS;
		}
	}
}

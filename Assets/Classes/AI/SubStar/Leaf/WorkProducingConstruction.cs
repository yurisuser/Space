using AI.AISubStar.ProdModule;

namespace AI.AISubStar
{
	public class WorkProducingConstruction : Leaf<SubStarBody>
	{
		public override EStateNode Tick(SubStarBody body)
		{
			for (int i = 0; i < body.industrialPointsArr.Length; i++)
			{
				ProducingModuleManager.Tick(body, body.industrialPointsArr[i].producingConstruction);
			}
			return EStateNode.SUCCESS;
		}
	}
}

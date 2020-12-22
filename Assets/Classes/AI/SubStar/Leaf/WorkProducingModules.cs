using AI.AISubStar.ProdModule;

namespace AI.AISubStar
{
	public class WorkProducingModules : Leaf<SubStarBody>
	{
		public override EStateNode Tick(SubStarBody body)
		{
			for (int i = 0; i < body.producingConstructionsArr.Length; i++)
			{
				ProducingModuleManager.Tick(body, body.producingConstructionsArr[i]);
			}
			return EStateNode.SUCCESS;
		}
	}
}

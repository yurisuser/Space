using AI.AISubStar.Manufacture;

namespace AI.AISubStar
{
	public class WorkProducingConstruction : Leaf<SubStarBody>
	{
		public override EStateNode Tick(SubStarBody body)
		{
			for (int i = 0; i < body.hub.industry.construction.Length; i++)
			{
				ManufactureManager.Tick(body, body.hub.industry.construction[i]);
			}
			return EStateNode.SUCCESS;
		}
	}
}

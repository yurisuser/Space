using AI.AISubStar.Manufacture;

namespace AI.AISubStar
{
	public class WorkProducingConstruction : Leaf<SubStarBody>
	{
		public override EStateNode Tick(SubStarBody body)
		{
			for (int i = 0; i < body.industry.industrialPointsArr.Length; i++)
			{
				ManufactureManager.Tick(body, body.industry.industrialPointsArr[i].manufactureConstruction);
			}
			return EStateNode.SUCCESS;
		}
	}
}

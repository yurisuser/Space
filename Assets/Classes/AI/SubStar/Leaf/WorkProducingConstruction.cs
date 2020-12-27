using AI.AISubStar.Manufacture;

namespace AI.AISubStar
{
	public class WorkProducingConstruction : Leaf<SubStarBody>
	{
		public override EStateNode Tick(SubStarBody body)
		{
			for (int i = 0; i < body.manufacture.industrialPointsArr.Length; i++)
			{
				ManufactureManager.Tick(body, body.manufacture.industrialPointsArr[i].manufactureConstruction);
			}
			return EStateNode.SUCCESS;
		}
	}
}

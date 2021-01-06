using AI.AISubStar.Manufacture;

namespace AI.AISubStar
{
	public class WorkProducingConstruction : Leaf<SubStarBody>
	{
		public override EStateNode Tick(SubStarBody body)
		{
			for (int i = 0; i < body.controlCentre.industry.construction.Length; i++)
			{
				ManufactureManager.Tick(body, body.controlCentre.industry.construction[i]);
			}
			return EStateNode.SUCCESS;
		}
	}
}

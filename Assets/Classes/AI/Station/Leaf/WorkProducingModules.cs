using AI.AIStation.ProdModule;

namespace AI.AIStation
{
	public class WorkProducingModules : Leaf<Station>
	{
		public override EStateNode Tick(Station s)
		{
			for (int i = 0; i < s.producingConstructions.Length; i++)
			{
				ProducingModuleManager.Tick(s, s.producingConstructions[i]);
			}
			return EStateNode.SUCCESS;
		}
	}
}

using AI.AIStation.ProdModule;

namespace AI.AIStation
{
	public class WorkProducingModules : Leaf<Station>
	{
		public override EStateNode Tick(Station s)
		{
			for (int i = 0; i < s.produceModuleArr.Length; i++)
			{
				ProducingModuleManager.Tick(s, s.produceModuleArr[i]);
			}
			return EStateNode.SUCCESS;
		}
	}
}

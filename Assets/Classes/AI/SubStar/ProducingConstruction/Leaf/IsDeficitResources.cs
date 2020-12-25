using System;

namespace AI.AISubStar.ProdModule
{
	public class IsDeficitResources : Condition<ProdConstructionWrapper>
	{
		public override EStateNode Tick(ProdConstructionWrapper wrapper)
		{
			if (wrapper.module.recipe.resources.Length == 0)
			{
				return EStateNode.FAILURE;
			}
			foreach (var item in wrapper.module.recipe.resources)
			{
				if (!wrapper.body.storage.isEnoughGoods(item)) return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}

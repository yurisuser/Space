using System;

namespace AI.AISubStar.Manufacture
{
	public class IsDeficitResources : Condition<ManufactureWrapper>
	{
		public override EStateNode Tick(ManufactureWrapper wrapper)
		{
			if (wrapper.module.recipe.resources.Length == 0)
			{
				return EStateNode.FAILURE;
			}
			foreach (var item in wrapper.module.recipe.resources)
			{
				if (!wrapper.body.hub.storage.isEnoughGoods(item)) return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}

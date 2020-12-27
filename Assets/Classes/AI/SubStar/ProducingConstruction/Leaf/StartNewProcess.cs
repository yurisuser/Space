using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.AISubStar.Manufacture
{
	public class StartNewProcess : Leaf<ManufactureWrapper>
	{
		public override EStateNode Tick(ManufactureWrapper wrapper)
		{
			wrapper.module.stageProcess = 1;
			WithdrawResources(wrapper);
			return EStateNode.SUCCESS;
		}

		private void WithdrawResources(ManufactureWrapper wrapper)
		{
			for (int i = 0; i < wrapper.module.recipe.resources.Length; i++)
			{
				wrapper.body.storage.Subtract(wrapper.module.recipe.resources[i]);
			}
		}
	}
}

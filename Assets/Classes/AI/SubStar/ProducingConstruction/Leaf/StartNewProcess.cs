using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.AISubStar.ProdModule
{
	public class StartNewProcess : Leaf<ProdConstructionWrapper>
	{
		public override EStateNode Tick(ProdConstructionWrapper wrapper)
		{
			wrapper.module.stageProcess = 1;
			WithdrawResources(wrapper);
			return EStateNode.SUCCESS;
		}

		private void WithdrawResources(ProdConstructionWrapper wrapper)
		{
			for (int i = 0; i < wrapper.module.recipe.resources.Length; i++)
			{
				wrapper.body.storage.Subtract(wrapper.module.recipe.resources[i]);
			}
		}
	}
}

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
			DeleteEmptyStacks(wrapper);
			return EStateNode.SUCCESS;
		}

		private void WithdrawResources(ProdConstructionWrapper wrapper)
		{
			for (int i = 0; i < wrapper.module.recipe.resources.Length; i++)
			{
				for (int c = 0; c < wrapper.body.storage.Length; c++)
				{
					if (wrapper.module.recipe.resources[i].id == wrapper.body.storage[c].id)
					{
						wrapper.body.storage[c].quantity -= wrapper.module.recipe.resources[i].quantity;
						if (wrapper.body.storage[c].quantity < 0) Debug.LogError("SubZero cargo quantity!");
					}
				}
			}
		}

		private void DeleteEmptyStacks(ProdConstructionWrapper w)
		{
			List<GoodsStack> result = new List<GoodsStack>();
			for (int i = 0; i < w.body.storage.Length; i++)
			{
				if (w.body.storage[i].quantity <= 0) continue;
				result.Add(w.body.storage[i]);
			}
			w.body.storage = result.ToArray();
		}
	}
}

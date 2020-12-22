using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.AIStation.ProdModule
{
	public class StartNewProcess : Leaf<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper wrapper)
		{
			wrapper.module.stageProcess = 1;
			WithdrawResources(wrapper);
			DeleteEmptyStacks(wrapper);
			return EStateNode.SUCCESS;
		}

		private void WithdrawResources(ProdModuleWrapper wrapper)
		{
			for (int i = 0; i < wrapper.module.recipe.resources.Length; i++)
			{
				for (int c = 0; c < wrapper.station.storage.Length; c++)
				{
					if (wrapper.module.recipe.resources[i].id == wrapper.station.storage[c].id)
					{
						wrapper.station.storage[c].quantity -= wrapper.module.recipe.resources[i].quantity;
						if (wrapper.station.storage[c].quantity < 0) Debug.LogError("SubZero cargo quantity!");
					}
				}
			}
		}

		private void DeleteEmptyStacks(ProdModuleWrapper w)
		{
			List<GoodsStack> result = new List<GoodsStack>();
			for (int i = 0; i < w.station.storage.Length; i++)
			{
				if (w.station.storage[i].quantity <= 0) continue;
				result.Add(w.station.storage[i]);
			}
			w.station.storage = result.ToArray();
		}
	}
}

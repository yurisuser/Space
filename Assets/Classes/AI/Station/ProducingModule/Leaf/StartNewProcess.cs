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
				for (int c = 0; c < wrapper.station.cargohold.Length; c++)
				{
					if (wrapper.module.recipe.resources[i].id == wrapper.station.cargohold[c].id)
					{
						wrapper.station.cargohold[c].quantity -= wrapper.module.recipe.resources[i].quantity;
						if (wrapper.station.cargohold[c].quantity < 0) Debug.LogError("SubZero cargo quantity!");
					}
				}
			}
		}

		private void DeleteEmptyStacks(ProdModuleWrapper w)
		{
			List<GoodsStack> result = new List<GoodsStack>();
			for (int i = 0; i < w.station.cargohold.Length; i++)
			{
				if (w.station.cargohold[i].quantity <= 0) continue;
				result.Add(w.station.cargohold[i]);
			}
			w.station.cargohold = result.ToArray();
		}
	}
}

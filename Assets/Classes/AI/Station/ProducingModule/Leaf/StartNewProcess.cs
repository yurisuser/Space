using System;
using UnityEngine;

namespace AI.AIStation.ProdModule
{
	public class StartNewProcess : Leaf<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper wrapper)
		{
			wrapper.module.stageProcess = 1;
			wrapper.module.state = EProducingState.work;
			WithdrawResources(wrapper);
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
	}
}

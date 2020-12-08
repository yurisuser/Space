using System;
using System.Linq;

namespace AI.AIStation.ProdModule
{
	public class AddProductToCargo : Leaf<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper wrapper)
		{
			foreach (var item in wrapper.module.recipe.production)
			{
				int i = Array.FindIndex(wrapper.station.cargohold, x => x.id == item.id);
				if (i >= 0)
				{
					wrapper.station.cargohold[i].quantity += item.quantity;
				}
				else
				{
					wrapper.station.cargohold = wrapper.station.cargohold.Concat(new GoodsStack[] { item }).ToArray();
				}
				wrapper.module.stageProcess = 0f;
			}
			wrapper.module.stageProcess = 0;
			wrapper.module.state = EProducingState.finished;
			return EStateNode.SUCCESS;
		}
	}
}

using System;

namespace AI.AIStation.ProdModule
{
	public class IsDeficitResources : Condition<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper wrapper)
		{
			foreach (var item in wrapper.module.recipe.resources)
			{
				if (!Array.Exists(wrapper.station.cargohold, x => x.id == item.id))
					return EStateNode.SUCCESS;
				if (Array.Exists(wrapper.station.cargohold, x => x.id == item.id && x.quantity < item.quantity)) 
					return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}

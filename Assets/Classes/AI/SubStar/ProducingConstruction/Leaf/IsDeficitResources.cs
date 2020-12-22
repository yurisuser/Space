﻿using System;

namespace AI.AISubStar.ProdModule
{
	public class IsDeficitResources : Condition<ProdConstructionWrapper>
	{
		public override EStateNode Tick(ProdConstructionWrapper wrapper)
		{
			foreach (var item in wrapper.module.recipe.resources)
			{
				if (!Array.Exists(wrapper.body.storage, x => x.id == item.id))
					return EStateNode.SUCCESS;
				if (Array.Exists(wrapper.body.storage, x => x.id == item.id && x.quantity < item.quantity)) 
					return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}

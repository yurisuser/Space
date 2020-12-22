using System;
using System.Linq;

namespace AI.AIStation.ProdModule
{
	public class FinishWork : Leaf<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper w)
		{
			w.module.stageProcess = 0;
			AddProducts(w);
			return EStateNode.SUCCESS;
		}
		private void AddProducts(ProdModuleWrapper w)
		{
			foreach (var item in w.module.recipe.production)
			{
				int i = Array.FindIndex(w.station.storage, x => x.id == item.id);
				if (i >= 0)
				{
					w.station.storage[i].quantity += item.quantity;
				}
				else
				{
					w.station.storage = w.station.storage.Concat(new GoodsStack[] { item }).ToArray();
				}
				w.module.stageProcess = 0f;
			}
		}
	}
}

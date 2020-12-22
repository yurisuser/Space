using System;
using System.Linq;

namespace AI.AISubStar.ProdModule
{
	public class FinishWork : Leaf<ProdConstructionWrapper>
	{
		public override EStateNode Tick(ProdConstructionWrapper w)
		{
			w.module.stageProcess = 0;
			AddProducts(w);
			return EStateNode.SUCCESS;
		}
		private void AddProducts(ProdConstructionWrapper w)
		{
			foreach (var item in w.module.recipe.production)
			{
				int i = Array.FindIndex(w.body.storage, x => x.id == item.id);
				if (i >= 0)
				{
					w.body.storage[i].quantity += item.quantity;
				}
				else
				{
					w.body.storage = w.body.storage.Concat(new GoodsStack[] { item }).ToArray();
				}
				w.module.stageProcess = 0f;
			}
		}
	}
}

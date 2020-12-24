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
				w.body.storage.Add(item);
			}
		}
	}
}

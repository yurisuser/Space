using System;
using System.Linq;

namespace AI.AISubStar.Manufacture
{
	public class FinishWork : Leaf<ManufactureWrapper>
	{
		public override EStateNode Tick(ManufactureWrapper w)
		{
			w.module.stageProcess = 0;
			AddProducts(w);
			return EStateNode.SUCCESS;
		}
		private void AddProducts(ManufactureWrapper w)
		{
			foreach (var item in w.module.recipe.production)
			{
				w.body.controlCentre.storage.Add(item);
			}
		}
	}
}

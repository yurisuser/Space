using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIStation.ProdModule
{
	public class IsNotLastStepProcess : Condition<ProdModuleWrapper>
	{
		public override EStateNode Tick(ProdModuleWrapper w)
		{
			if (w.module.stageProcess < w.module.recipe.duration)
			{
				return EStateNode.FAILURE;
			}
			return EStateNode.SUCCESS;
		}
	}
}

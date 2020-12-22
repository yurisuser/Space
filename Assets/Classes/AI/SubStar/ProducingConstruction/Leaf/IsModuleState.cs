using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AISubStar.ProdModule
{
	public class IsModuleState : Condition<ProdConstructionWrapper>
	{
		private EProducingState state;

		private IsModuleState() { }
		public IsModuleState(EProducingState state)
		{
			this.state = state;
		}
		public override EStateNode Tick(ProdConstructionWrapper w)
		{
			if (w.module.state == state) return EStateNode.SUCCESS;
			return EStateNode.FAILURE;
		}
	}
}

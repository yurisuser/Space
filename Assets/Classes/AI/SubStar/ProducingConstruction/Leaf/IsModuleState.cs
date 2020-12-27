using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AISubStar.Manufacture
{
	public class IsModuleState : Condition<ManufactureWrapper>
	{
		private EProducingState state;

		private IsModuleState() { }
		public IsModuleState(EProducingState state)
		{
			this.state = state;
		}
		public override EStateNode Tick(ManufactureWrapper w)
		{
			if (w.module.state == state) return EStateNode.SUCCESS;
			return EStateNode.FAILURE;
		}
	}
}

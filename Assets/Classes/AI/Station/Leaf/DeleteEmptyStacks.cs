using System.Collections.Generic;

namespace AI.AIStation
{
	public class DeleteEmptyStacks : Leaf<Station>
	{
		public override EStateNode Tick(Station st)
		{
			List<GoodsStack> result = new List<GoodsStack>();
			for (int i = 0; i < st.cargohold.Length; i++)
			{
				if (st.cargohold[i].quantity <= 0) continue;
				result.Add(st.cargohold[i]);
			}
			st.cargohold = result.ToArray();
			return EStateNode.SUCCESS;
		}
	}
}

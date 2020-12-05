using System;
using System.Linq;
using static Data;

public static class StationProducer
{
	private static Station st;
	public static void Tick(int starIndex, int stationIndex)
	{
		st = Galaxy.StarSystemsArr[starIndex].StationArr[stationIndex];
		for (int i = 0; i < st.produceModuleArr.Length; i++)
		{
			Do(st.produceModuleArr[i], st.cargohold);
		}
		Galaxy.StarSystemsArr[starIndex].StationArr[stationIndex] = st;
	}
	private static bool CheckRecipeResources(ProductRecipe recipe, GoodsStack[] cargo)
	{
		foreach (var item in recipe.resources)
		{
			if (!Array.Exists(cargo, x => x.id == item.id && x.quantity <= item.quantity)) return false;
		}
		return true;
	}

	private static void Do(ProduceModule pm, GoodsStack[] cargo)
	{
		if (pm.status == EProduceStatus.work) Work(pm, cargo);

		if (!CheckRecipeResources(pm.recipe, cargo))
		{
			pm.status = EProduceStatus.deficitRecorces;
			return;
		}

		if (pm.status == EProduceStatus.empty)
		{
			StartNewProcess(pm, cargo);
			return;
		}
	}

	private static void StartNewProcess(ProduceModule pm, GoodsStack[] cargo)
	{
		for (int i = 0; i < pm.recipe.resources.Length; i++)
		{
			var rescargoRes = Array.FindIndex(cargo, x => pm.recipe.resources[i].id == x.id);
			cargo[rescargoRes].quantity -= pm.recipe.resources[i].quantity;

			//if (cargo[rescargoRes].quantity <= 0) DeleteStack(cargo[rescargoRes]);
		}
	}

	private static void Work(ProduceModule pm, GoodsStack[] cargo)
	{

	}

	private static void DeleteStack(GoodsStack stack, GoodsStack[] cargo)
	{
		
	}
}


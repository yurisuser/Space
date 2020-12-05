using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Data;

public static class StationProducer
{
	private static Station st;
	public static void Tick(int starIndex, int stationIndex)
	{
		st = Galaxy.StarSystemsArr[starIndex].StationArr[stationIndex];
		for (int i = 0; i < st.produceModuleArr.Length; i++)
		{
			Do(i);
		}
		DeleteEmptyStacks();
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

	private static void Do(int indexModule)
	{

		for (int i = 0; i < st.cargohold.Length; i++)
		{
			st.cargohold[i].quantity -= 3000;
		}

		if (st.produceModuleArr[indexModule].status == EProduceStatus.work) Work(indexModule);

		if (!CheckRecipeResources(st.produceModuleArr[indexModule].recipe, st.cargohold))
		{
			st.produceModuleArr[indexModule].status = EProduceStatus.deficitRecorces;
			return;
		}

		if (st.produceModuleArr[indexModule].status == EProduceStatus.empty)
		{
			StartNewProcess(indexModule);
			return;
		}
	}

	private static void StartNewProcess(int indexModule)
	{
		for (int i = 0; i < st.produceModuleArr[indexModule].recipe.resources.Length; i++)
		{
			var indexResource = Array.FindIndex(st.cargohold, x => st.produceModuleArr[indexModule].recipe.resources[i].id == x.id);
			st.cargohold[indexResource].quantity -= st.produceModuleArr[indexModule].recipe.resources[i].quantity;
		}
	}

	private static void Work(int indexModule)
	{

	}

	private static void DeleteEmptyStacks()
	{
		List<GoodsStack> result = new List<GoodsStack>();
		for (int i = 0; i < st.cargohold.Length; i++)
		{
			if (st.cargohold[i].quantity <= 0) continue;
			result.Add(st.cargohold[i]);
		}
		st.cargohold = result.ToArray();
	}
}


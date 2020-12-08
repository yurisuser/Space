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
		Utilities.ShowMeObject(Galaxy.StarSystemsArr[11].StationArr[0].cargohold);
	}

	private static void Do(int indexModule)
	{
		if (st.produceModuleArr[indexModule].state == EProducingState.pause) return;

		if (st.produceModuleArr[indexModule].state == EProducingState.work)
		{//если запущено, продолжать
			Work(indexModule); 
			return;
		}

		if (IsDeficitResources(st.produceModuleArr[indexModule].recipe, st.cargohold))
		{//если русурсов мало, отказ
			st.produceModuleArr[indexModule].state = EProducingState.deficitRecorces;
			return;
		}

		if (st.produceModuleArr[indexModule].state == EProducingState.finished)
		{// если незапущено, запустить
			StartNewProcess(indexModule);
			if (st.produceModuleArr[indexModule].recipe.duration == st.produceModuleArr[indexModule].stageProcess)
				FinishProcess(indexModule);
			return;
		}


	}

	private static bool IsDeficitResources(ProductRecipe recipe, GoodsStack[] cargo)
	{
		foreach (var item in recipe.resources)
		{
			//if (Array.Exists(cargo, x => x.id == item.id)) && x.quantity <= item.quantity)) return true;
			for (int r = 0; r < recipe.resources.Length; r++)
			{
				for (int c = 0; c < cargo.Length; c++)
				{

				}
			}
		}
		return false;
	}

	private static void StartNewProcess(int indexModule)
	{
		WithdrawResources(indexModule);
		st.produceModuleArr[indexModule].stageProcess = 1;
		st.produceModuleArr[indexModule].state = EProducingState.work;
	}

	private static void Work(int indexModule)
	{
		if (st.produceModuleArr[indexModule].stageProcess < st.produceModuleArr[indexModule].recipe.duration)
		{
			st.produceModuleArr[indexModule].stageProcess++;
		}
		if (st.produceModuleArr[indexModule].stageProcess >= st.produceModuleArr[indexModule].recipe.duration)
		{
			FinishProcess(indexModule);
		}
	}

	private static void FinishProcess(int indexModule)
	{
		AddProducts(indexModule);
		st.produceModuleArr[indexModule].stageProcess = 0;
		st.produceModuleArr[indexModule].state = EProducingState.finished;
	}

	private static void WithdrawResources(int indexModule)
	{
		for (int i = 0; i < st.produceModuleArr[indexModule].recipe.resources.Length; i++)
		{
			for (int c = 0; c < st.cargohold.Length; c++)
			{
				if(st.produceModuleArr[indexModule].recipe.resources[i].id == st.cargohold[c].id)
				{
					st.cargohold[c].quantity -= st.produceModuleArr[indexModule].recipe.resources[i].quantity;
					if (st.cargohold[c].quantity < 0) Debug.LogError("SubZero cargo quantity!");
				}
			}
		}
	}

	private static void AddProducts(int indexModule)
	{
		foreach (var item in st.produceModuleArr[indexModule].recipe.production)
		{
			int i = Array.FindIndex(st.cargohold, x => x.id == item.id);
			if (i >= 0)
			{
				st.cargohold[i].quantity += item.quantity;
			}
			else
			{
				st.cargohold = st.cargohold.Concat(new GoodsStack[] { item }).ToArray();
			}
			st.produceModuleArr[indexModule].stageProcess = 0f;
		}
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


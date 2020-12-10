﻿using UnityEngine;

public static class StationCreator
{
	public static Station[] CreateTestStation(int stationAmaunt)
	{
		Station[] result = new Station[stationAmaunt];
		for (int i = 0; i < stationAmaunt; i++)
		{
			Station st = new Station
			{
				position = GetRNDPosition(),
				cargohold = TestAddResources(8),
				produceModuleArr = TestAddProduceModule()
			};
			result[i] = st;
		}
		return result;
	}

	private static Vector3 GetRNDPosition()
	{
		return new Vector3(
			Random.Range(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
			Random.Range(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
			Settings.StarSystem.SYSTEM_STATIONS_LAYER
			);
	}

	private static GoodsStack[] TestAddResources(int count)
	{
		GoodsStack[] result = new GoodsStack[Data.goodsArr.Length];
		for (int i = 0; i < Data.goodsArr.Length; i++)
		{
			result[i] = new GoodsStack
			{
				id = Data.goodsArr[i].id,
				quantity = count
			};
		}
		return result;
	}

	private static ProducingModule[] TestAddProduceModule()
	{
		return new ProducingModule[] {
			new ProducingModule{
				recipe = Data.productRecipeArr[1],
				stageProcess = 0,
				state = EProducingState.finished
			}
		};
	}
}
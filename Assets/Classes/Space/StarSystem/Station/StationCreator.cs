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
				position = GetRNDPosition()
			};
			st.hub = new Hub(st);
		result[i] = st;
		}
		return result;
	}

	private static Vector3 GetRNDPosition()
	{
		return new Vector3(
			Rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
			Rnd.Next(-Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM, Settings.StarSystem.MAX_RADIUS_STAR_SYSTEM),
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
				amount = count
			};
		}
		return result;
	}

	private static IndustryConstruction[] TestAddIdustryConstruction()
	{
		return new IndustryConstruction[] {
			new IndustryConstruction {
					recipe = Data.productRecipeArr[0],
					stageProcess = 0,
					state = EProducingState.finished
			} 
		};
	}
}
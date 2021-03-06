﻿using System;
using UnityEngine;

public class StationSysMapScr : MonoBehaviour
{
    private int starIndex;
    private int stationIndex;
    private Station st;

	private void OnMouseDown()
	{
        Debug.Log("-----" + Rnd.Next(0,100));
        var orders = st.hub.market.GetAllOffers();
		foreach (var item in orders)
		{
            Debug.Log($" id {item.goodsId}  amount       {item.goodsAmount} / {item.limit} = {item.fullness}       price {item.goodsPrice}");
		}
 
		//Utilities.ShowMeObject(st.industry);
		//Utilities.ShowMeObject(st.storage.GetStorage());
		//Utilities.ShowMeObject(st.market);
    }

	public void SetIndexes(int starIndex, int stationIndex)
	{
        this.starIndex = starIndex;
        this.stationIndex = stationIndex;
        st = Galaxy.StarSystemsArr[starIndex].StationArr[stationIndex];
    }
    private void Show()
    {
        st = Galaxy.StarSystemsArr[starIndex].StationArr[0];
        int prodId = st.hub.industry.construction[0].recipe.production[0].id;
        int indexProd = Array.FindIndex(st.hub.storage.GetStorage(), x => x.id == prodId);
    }
}

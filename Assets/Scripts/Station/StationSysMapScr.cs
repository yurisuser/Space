using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSysMapScr : MonoBehaviour
{
    private int starIndex;
    private int stationIndex;
    private Station st;
    void Start()
    {
        
    }
    void Update()
    {
        if (stationIndex == 0)
            Show();
    }

	private void OnMouseDown()
	{
        Utilities.ShowMeObject(st.storage.goodsArr);
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
        int prodId = st.manufacture.industrialPointsArr[0].producingConstruction.recipe.production[0].id;
        int indexProd = Array.FindIndex(st.storage.goodsArr, x => x.id == prodId);
    }
}

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

    public void SetIndexes(int starIndex, int stationIndex)
	{
        this.starIndex = starIndex;
        this.stationIndex = stationIndex;
        st = Galaxy.StarSystemsArr[starIndex].StationArr[stationIndex];
    }
    private void Show()
    {        
        Utilities.ShowMe(1, $"Recipe {st.industrialPointsArr[0].producingConstruction.recipe.name}");
        st = Galaxy.StarSystemsArr[starIndex].StationArr[0];
        int prodId = st.industrialPointsArr[0].producingConstruction.recipe.production[0].id;
        int indexProd = Array.FindIndex(st.storage, x => x.id == prodId);
        if (st.storage.Length > 0)
            Utilities.ShowMe(2, $"item: {st.storage[indexProd].id} q: {st.storage[indexProd].quantity}");
        else
            Utilities.ShowMe(2, "null");
    }
}

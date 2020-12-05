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
        Utilities.ShowMe(1, $"Recipe {st.produceModuleArr[0].recipe.name}");
        st = Galaxy.StarSystemsArr[starIndex].StationArr[0];
        int prodId = st.produceModuleArr[0].recipe.production[0].id;
        int indexProd = Array.FindIndex(st.cargohold, x => x.id == prodId);
        if (st.cargohold.Length > 0)
            Utilities.ShowMe(2, $"item: {st.cargohold[indexProd].id} q: {st.cargohold[indexProd].quantity}");
        else
            Utilities.ShowMe(2, "null");
    }
}

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
        Utilities.ShowMeObject(st.industry);
        Utilities.ShowMeObject(st.storage.GetStorage());
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
        int prodId = st.industry.construction[0].recipe.production[0].id;
        int indexProd = Array.FindIndex(st.storage.GetStorage(), x => x.id == prodId);
    }
}

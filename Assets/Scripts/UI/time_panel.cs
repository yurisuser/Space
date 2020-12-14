using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time_panel : MonoBehaviour
{
    private GameObject panel;
    private Gmgr gmgr;
    private Text textGo;
    private Text elapsedTime;
    void Start()
    {
        gmgr = GameObject.Find("GMGR").GetComponent<Gmgr>();
        textGo = transform.GetChild(0).gameObject.GetComponent<Text>();
        elapsedTime = transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        textGo.text ="day: " + Turner.GetCurrentTime().ToString();
        if (Turner.allowedTimeSteps <= 0)
		{
            elapsedTime.text = "";
		} else
		{
            elapsedTime.text = Turner.allowedTimeSteps.ToString();
		}        
	}

    public void OnceHandler()
	{
        Turner.GoStep();
	}

    public void StreamHandler()
	{
        Turner.GoStream();
    }

    public void PauseHandler()
	{
        Turner.Stop();
    }
}

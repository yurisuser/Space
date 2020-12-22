﻿using AI;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gmgr : MonoBehaviour
{
	public static Gmgr gmgr;
	private SceneState sceneState;

	void Start()
	{
		gmgr = this;
		Data.Init();
		PrefabService.Init();
		GalaxyCreator.CreateGalaxy();
		///
		//LoadSceneGalaxy();
		LoadSceneStarSystem(11);
		///
		Turner.TimeTrigger += TurnUpdate;
		Turner.GoStream();
		TaskManager.Tick();// initial tick
	}

	private void Update()
	{
		Turner.Update();
		Utilities.ShowMe(5, TaskManager.isAllFinished);
	}

	private void TurnUpdate()
	{
		Debug.Log("Turner update tick");
		TaskManager.Tick();
		Utilities.ShowMeObject(Galaxy.StarSystemsArr[11].StationArr[0].storage);
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (gmgr == null)
		{
			gmgr = this;
		} else {
			Object.Destroy(gameObject);
		}
	}

	public void LoadSceneGalaxy()
	{
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("Galaxy");
		sceneState = new SceneStateGalaxy();
	}

	public void LoadSceneStarSystem(int idStarSystem)
	{
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("StarSystem");
		sceneState = new SceneStateStarSystem(Galaxy.StarSystemsArr[idStarSystem]);
	}

	private void OnDrawMapAfterLoadScene(Scene arg0, LoadSceneMode arg1)
	{
		sceneState.DrawScene();
		SceneManager.sceneLoaded -= OnDrawMapAfterLoadScene;
	}
}

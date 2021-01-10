using UnityEngine;
using UnityEngine.SceneManagement;

public class Gmgr : MonoBehaviour
{
	public static Gmgr gmgr;
	public static int currentSystemIndex = 0;
	public static EScene currentScene;

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
		//TaskManager.Tick();// initial tick
	}

	private void Update()
	{
		UI.Escaper.Update();
		sceneState.Update();
		Turner.Update();
	}

	private void TurnUpdate()
	{
		Debug.Log("Turner update tick");
		sceneState.Tick();
		TaskManager.Tick();
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (gmgr == null)
		{
			gmgr = this;
		} else {
			Destroy(gameObject);
		}
	}

	public void LoadSceneGalaxy()
	{
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("Galaxy");
		currentScene = EScene.galaxy;
		sceneState = new SceneStateGalaxy();
	}

	public void LoadSceneStarSystem(int indexStarSystem)
	{
		currentSystemIndex = indexStarSystem;
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("StarSystem");
		currentScene = EScene.starSystem;
		sceneState = new SceneStateStarSystem(Galaxy.StarSystemsArr[indexStarSystem]);
		UI.Escaper.Add(LoadSceneGalaxy);
	}

	private void OnDrawMapAfterLoadScene(Scene arg0, LoadSceneMode arg1)
	{
		sceneState.DrawScene();
		SceneManager.sceneLoaded -= OnDrawMapAfterLoadScene;
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Gmgr : MonoBehaviour
{
	public static Gmgr gmgr;

	private SceneState sceneState;
	private static bool isEmptyTick = true; //пустой тик для отработки сцены перед запуском TaskManager
	private static bool isDateChanged = false;

	void Start()
	{
		gmgr = this;
		Data.Init();
		PrefabService.Init();
		GalaxyCreator.CreateGalaxy();
		LoadScene();
		Turner.TimeTrigger += TurnUpdate;
		Turner.GoStream();
	}

	private void LoadScene()
	{
		///
		//LoadSceneGalaxy();

		Location loc = new Location();
		loc.indexStarSystem = 55;
		loc.indexPlanetSystem = 0;
		loc.elocation = ELocation.space;
		Glob.sceneLocation = loc;
		LoadSceneStarSystem();

		//Location loc = new Location();
		//loc.indexStarSystem = 66;
		//loc.indexPlanetSystem = 0;
		//loc.elocation = ELocation.planet;
		//LoadSceneSystemViever(loc);

		///
	}
	private void LateUpdate()
	{
		//все дожно считаться после того, как отработала сцена
		UI.Escaper.LateUpdate();
		sceneState.LateUpdate();
		Turner.LateUpdate();
		UpdateAfterEmptyTick();
	}

	private void TurnUpdate()
	{
		isDateChanged = true;
	}

	private void UpdateAfterEmptyTick()
	{
		if (!isDateChanged) return;
		if (isEmptyTick)
		{
			isEmptyTick = false;
			return;
		}
		sceneState.Tick();
		TaskManager.Tick();

		isDateChanged = false;
		isEmptyTick = true;


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
		Glob.currentScene = EScene.galaxy;
		sceneState = new SceneStateGalaxy();
	}

	public void LoadSceneStarSystem()
	{
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("StarSystem");
		Glob.currentScene = EScene.starSystem;
		sceneState = new SceneStateStarSystem(Galaxy.StarSystemsArr[Glob.sceneLocation.indexStarSystem]);
		UI.Escaper.Add(LoadSceneGalaxy);
	}

	public void LoadSceneSystemViever(Location location)
	{
		Glob.sceneLocation = location;
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("SystemViewer");
		Glob.currentScene = EScene.systemViewer;
		sceneState = new SceneStateSystemViewer(location);
		UI.Escaper.Add(LoadSceneStarSystem);
	}

	private void OnDrawMapAfterLoadScene(Scene arg0, LoadSceneMode arg1)
	{
		sceneState.DrawScene();
		SceneManager.sceneLoaded -= OnDrawMapAfterLoadScene;
	}
}

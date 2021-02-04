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
		LoadScene();
		Turner.TimeTrigger += TurnUpdate;
		Turner.GoStream();
	}

	private void LoadScene()
	{
		///
		//LoadSceneGalaxy();

		//Glob.currentPlanetSystemIndex = 11;
		//LoadSceneStarSystem();

		Location loc = new Location();
		loc.indexStarSystem = 11;
		loc.indexPlanetSystem = 0;
		loc.elocation = ELocation.planet;
		LoadScenePlanet(loc);

		///
	}

	private void LateUpdate()
	{
		//все дожно считаться после того, как отработала сцена
		UI.Escaper.LateUpdate();
		sceneState.LateUpdate();
		Turner.LateUpdate();
	}

	private void TurnUpdate()
	{
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
		Glob.currentScene = EScene.galaxy;
		sceneState = new SceneStateGalaxy();
	}

	public void LoadSceneStarSystem()
	{
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("StarSystem");
		Glob.currentScene = EScene.starSystem;
		sceneState = new SceneStateStarSystem(Galaxy.StarSystemsArr[Glob.currentStarSystemIndex]);
		UI.Escaper.Add(LoadSceneGalaxy);
	}

	public void LoadScenePlanet(Location location)
	{
		Glob.currentPlanetSystemIndex = location.indexPlanetSystem;
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("PlanetScene");
		Glob.currentScene = EScene.planetScene;
		sceneState = new SceneStatePlanetView(location);
		UI.Escaper.Add(LoadSceneStarSystem);

	}

	private void OnDrawMapAfterLoadScene(Scene arg0, LoadSceneMode arg1)
	{
		sceneState.DrawScene();
		SceneManager.sceneLoaded -= OnDrawMapAfterLoadScene;
	}
}

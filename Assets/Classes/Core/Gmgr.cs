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
		LoadSceneGalaxy();
		//LoadSceneStarSystem(11);
		///
		Turner.TimeTrigger += TurnUpdate;
		Turner.GoStream();
		//TaskManager.Tick();// initial tick
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
		Utilities.ShowMe(3, $"cur sys {Glob.currentSystemIndex}");
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

	public void LoadSceneStarSystem(int indexStarSystem)
	{
		Glob.currentSystemIndex = indexStarSystem;
		SceneManager.sceneLoaded += OnDrawMapAfterLoadScene;
		SceneManager.LoadScene("StarSystem");
		Glob.currentScene = EScene.starSystem;
		sceneState = new SceneStateStarSystem(Galaxy.StarSystemsArr[indexStarSystem]);
		UI.Escaper.Add(LoadSceneGalaxy);
	}

	private void OnDrawMapAfterLoadScene(Scene arg0, LoadSceneMode arg1)
	{
		sceneState.DrawScene();
		SceneManager.sceneLoaded -= OnDrawMapAfterLoadScene;
	}
}

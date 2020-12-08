using AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gmgr : MonoBehaviour
{
	public static Gmgr gmgr;
	private SceneState sceneState;
	public Turner turner;

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
		turner = Turner.getInstance();
		turner.TimeTrigger += TurnUpdate;
		turner.GoStream();		
		AIManager.Tick(); // initial tick
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
		if (turner != null)
			turner.Tick();	
	}

	private void TurnUpdate()
	{
		Debug.Log("Turner update tick");
		AIManager.Tick();
		StationManager.Tick();
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

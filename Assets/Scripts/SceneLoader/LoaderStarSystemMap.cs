using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderStarSystemMap : MonoBehaviour
{
	private StarSystem starSystem;
	public void Load(StarSystem starSystem)
	{
		this.starSystem = starSystem;
		Draw();
	}

	private void Draw()
	{
		GameObject folder = new GameObject { name = "System" };

			GameObject star = Instantiate(
				PrefabService.StarsGalaxyMap[starSystem.star.type],
				new Vector3(0, 0, -10),
				Quaternion.identity);
			star.transform.SetParent(folder.transform);
	}
}

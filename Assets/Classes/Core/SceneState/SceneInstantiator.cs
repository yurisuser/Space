using UnityEngine;

public static class SceneInstantiator
{
	public static void AddShip(Ship ship, Transform parent)
	{
		GameObject go = GameObject.Instantiate(
				Resources.Load("Prefabs/Ships/TestShip") as GameObject,
				ship.position,
				Quaternion.identity);
		go.transform.SetParent(parent);
		go.GetComponent<ShipScr>().SetShip(ship);
	}
}

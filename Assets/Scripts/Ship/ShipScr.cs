using UnityEngine;
using System.Collections;
using System;
using AI.AIShip;
using UnityEngine.EventSystems;

public class ShipScr : MonoBehaviour
{
	public int shipId; //for scene

	private Order order;
	private GameObject destOrderBeacon;
	private GameObject destStepBeacon;
	private int currentTurn;
	private float progress = 0;
	private Ship ship;
	void Start(){}

	public void SetShip(Ship ship)
	{
		currentTurn = Turner.GetCurrentTime();
		this.ship = ship;
		shipId = ship.id;
		order = ship.order.Clone();
		DrawDestinations();
	}
	void Update()
	{
		CheckNextTurn();
		Move();
		MoveNavigationPoints();		
	}

	private void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		Debug.Log($"Ship id {ship.id} currSys {ship.location.indexStarSystem} destSys {ship.order.destinationSystemIndex}");
		Debug.Log($"locSys {ship.location.indexStarSystem} ");
	}
	private void CheckNextTurn()
	{
		if (ship.state == EShipState.unHypering) return;
		if (currentTurn < Turner.GetCurrentTime())
		{
			if (ship.state == EShipState.docked || ship.state == EShipState.inHyper) GameObject.Destroy(gameObject);
			progress = 0;
			currentTurn = Turner.GetCurrentTime();
			order = ship.order.Clone();
		}
	}

	private void DrawDestinations()
	{
		destOrderBeacon = GameObject.Instantiate(
				Resources.Load("Prefabs/Ships/ordDest") as GameObject,
				order.destinationOrder,
				Quaternion.identity);

		destStepBeacon = GameObject.Instantiate(
		Resources.Load("Prefabs/Ships/TestShipOrderDest") as GameObject,
		order.destinationOrder,
		Quaternion.identity);
		destStepBeacon.transform.localScale *= 50;
	}
	private void Move()
	{
		transform.position = order.currentPosition;
		transform.position = Vector3.Lerp(order.currentPosition, order.destinationStep, progress);
		progress += Time.deltaTime / Turner.turn_length;
	}

	private void MoveNavigationPoints()
	{
		if (destOrderBeacon.transform.position != order.destinationOrder)
			destOrderBeacon.transform.position = order.destinationOrder;
		if (destStepBeacon.transform.position != order.destinationStep)
			destStepBeacon.transform.position = order.destinationStep;
	}

	private void OnDestroy()
	{
		Destroy(destStepBeacon);
		Destroy(destOrderBeacon);
	}
}

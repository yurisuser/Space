﻿using UnityEngine;
using System.Collections;
using System;
using AI.AIShip;

public class ShipScr : MonoBehaviour
{
	private Order order;
	private GameObject destOrderBeacon;
	private GameObject destStepBeacon;
	private int currentTurn;
	private float progress = 0;
	private Ship ship;
	public int shipId; //for scene
	void Start()
	{

		if (ship.id == 1018)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		}
	}

	public void SetShip(Ship ship)
	{
		this.ship = ship;
		shipId = ship.id;
		order = ship.order.Clone();
		DrawDestinations();
	}
	void Update()
	{
		CheckEndTurn();
		Move();
		MoveNavigationPoints();
	}

	private void CheckEndTurn()
	{
		if (currentTurn != Turner.GetCurrentTime())
		{
			if (ship.state != EShipState.inSpace) DestroyGO();
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

	private void DestroyGO()
	{
		Destroy(destStepBeacon);
		Destroy(destOrderBeacon);
		Destroy(gameObject);
	}
}

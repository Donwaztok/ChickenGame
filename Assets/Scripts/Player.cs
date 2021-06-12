using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

	[SerializeField] float moveSpeed = 10;
	[SerializeField] InputMaster controller;

	Rigidbody myRigidbody;
	EggDetector eggDetector;
	Egg egg;
	Vector2 direction;

	private void Awake() {
		controller = new InputMaster();
		controller.Player.Interact.performed += ctx => GetEgg(eggDetector.GetEgg());
		controller.Player.Movement.performed += ctx => setMove(ctx.ReadValue<Vector2>());

		controller.Player.Interact.canceled += ctx => GetEgg(null);
		controller.Player.Movement.canceled += ctx => setMove(new Vector2(0, 0));
	}

	void Start() {
		myRigidbody = GetComponent<Rigidbody>();
		eggDetector = GetComponentInChildren<EggDetector>();
	}

	void Update() {
		Move();
		if (egg) egg.transform.position = GameObject.Find("HandSlot").transform.position;
	}

	private void OnEnable() {
		controller.Enable();
	}

	private void OnDisable() {
		controller.Disable();
	}

	private void setMove(Vector2 direction) {
		this.direction = direction;
	}

	private void GetEgg(Egg egg) {
		this.egg = egg;
	}

	private void Move() {
		Vector3 movement = new Vector3(direction.x, 0, direction.y).normalized;
		if (movement == Vector3.zero)
			return;

		Quaternion rotation = Quaternion.LookRotation(movement);
		rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3600 * moveSpeed * Time.deltaTime);

		myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.deltaTime);
		myRigidbody.MoveRotation(rotation);
	}
}

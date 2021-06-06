using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float moveSpeed = 1;

	Rigidbody myRigidbody;
	EggDetector eggDetector;
	Egg egg;

	void Start() {
		myRigidbody = GetComponent<Rigidbody>();
		eggDetector = GetComponentInChildren<EggDetector>();
	}

	void Update() {
		Move();
		GetEgg();
	}

	private void Move() {
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

		if (movement == Vector3.zero)
			return;

		Quaternion rotation = Quaternion.LookRotation(movement);
		rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3600 * moveSpeed * Time.deltaTime);

		myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.deltaTime);
		myRigidbody.MoveRotation(rotation);
	}

	private void GetEgg() {
		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Jump")) {
			egg = eggDetector.GetEgg();
			if (egg) {
				egg.GetComponent<SphereCollider>().enabled = false;
				egg.GetComponent<Rigidbody>().useGravity = false;
				egg.transform.parent = GameObject.Find("HandSlot").transform;
			}
		} else if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2") || Input.GetButtonUp("Jump")) {
			if (egg) {
				egg.transform.parent = null;
				egg.GetComponent<Rigidbody>().useGravity = true;
				egg.GetComponent<SphereCollider>().enabled = true;
			}
			egg = null;
		}

		if (egg)
			egg.transform.position = GameObject.Find("HandSlot").transform.position;
	}
}

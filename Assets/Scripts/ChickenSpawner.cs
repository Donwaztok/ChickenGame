using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour {

	[SerializeField] GameObject chicken;
	[SerializeField] int secToSpawnChicken = 20;

	float chickenSpawnX = -8.2f;
	float chickenSpawnY = 4;
	Coroutine chickenSpawner;

	// Start is called before the first frame update
	void Start() {
		chickenSpawner = StartCoroutine(SpawnChickenContinuously());
	}

	// Update is called once per frame
	void Update() {

	}

	IEnumerator SpawnChickenContinuously() {
		while (true) {
			GameObject chicken = Instantiate(
				this.chicken,
				new Vector3(chickenSpawnX, chickenSpawnY, 4),
				Quaternion.identity
			) as GameObject;
			chickenSpawnX += 1.1f;
			if (chickenSpawnX >= 8.5) {
				chickenSpawnX = -8.2f;
				chickenSpawnY += 1.5f;
			}
			yield return new WaitForSeconds(secToSpawnChicken);
		}
	}
}

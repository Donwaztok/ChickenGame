using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {

	[SerializeField] GameObject egg;
	[SerializeField] int secToSpawnEgg = 10;
	[SerializeField] int secToFood = 20;

	Coroutine eggSpawner;
	Coroutine chickenEating;
	GameEngine gameEngine;

	void Start() {
		chickenEating = StartCoroutine(EatContinuously());
		eggSpawner = StartCoroutine(DropEggContinuously());
		gameEngine = FindObjectOfType<GameEngine>();
	}

	IEnumerator DropEggContinuously() {
		while (true) {
			GameObject egg = Instantiate(
				this.egg,
				new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),
				Quaternion.identity
			) as GameObject;
			yield return new WaitForSeconds(secToSpawnEgg);
		}
	}

	IEnumerator EatContinuously() {
		while (true) {
			if (gameEngine) {
				gameEngine.ChickenEating();
			}
			yield return new WaitForSeconds(secToFood);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {

	[SerializeField] GameObject egg;
	[SerializeField] int secToSpawnEgg = 10;

	Coroutine eggSpawner;


	// Start is called before the first frame update
	void Start() {
		eggSpawner = StartCoroutine(DropEggContinuously());
	}

	// Update is called once per frame
	void Update() {

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
}

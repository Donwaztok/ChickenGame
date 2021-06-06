using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggStore : MonoBehaviour {

    GameEngine gameEngine;

    private void Start() {
        gameEngine = FindObjectOfType<GameEngine>();
    }

	private void OnTriggerEnter(Collider other) {
		Egg egg = other.gameObject.GetComponent<Egg>();
		if (egg) {
			Destroy(other.gameObject);
			gameEngine.SellEgg();
		}
	}
}

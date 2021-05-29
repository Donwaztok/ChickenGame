using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameEngine : MonoBehaviour {

	[SerializeField] GameObject chicken;
	[SerializeField] int secToSpawnChicken = 20;
	[SerializeField] float initialMoney = 0;
	[SerializeField] float moneyPerEgg = 5;
	[Header("UI")]
	[SerializeField] TextMeshProUGUI chickenText;
	[SerializeField] TextMeshProUGUI coinText;

	float chickenSpawnX = -8.2f;
	float chickenSpawnY = 4;
	float money = 0;
	int chickensQty = 0;
	Coroutine chickenSpawner;

	void Start() {
		chickenSpawner = StartCoroutine(SpawnChickenContinuously());
		money = initialMoney;
		UpdateUI();
	}

	private void UpdateUI() {
		chickenText.text = chickensQty.ToString();
		coinText.text = "$ " + money.ToString();
	}

	IEnumerator SpawnChickenContinuously() {
		while (true) {
			GameObject chicken = Instantiate(
				this.chicken,
				new Vector3(chickenSpawnX, chickenSpawnY, 4),
				Quaternion.identity
			) as GameObject;
			chickensQty++;
			UpdateUI();
			chickenSpawnX += 1.1f;
			if (chickenSpawnX >= 8.5) {
				chickenSpawnX = -8.2f;
				chickenSpawnY += 1.5f;
			}
			yield return new WaitForSeconds(secToSpawnChicken);
		}
	}

	public void SellEgg() {
		money += moneyPerEgg;
		UpdateUI();
	}
}

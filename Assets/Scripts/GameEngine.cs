using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameEngine : MonoBehaviour {

	[SerializeField] GameObject chicken;
	[SerializeField] int secToSpawnChicken = 30;
	[SerializeField] int initialChickenQty = 5;
	[SerializeField] float foodPrice = 10;
	[SerializeField] float initialMoney = 0;
	[SerializeField] float moneyPerEgg = 5;
	[Header("UI")]
	[SerializeField] TextMeshProUGUI chickenText;
	[SerializeField] TextMeshProUGUI coinText;
	[SerializeField] TextMeshProUGUI timerText;

	int chickensQty = 0;
	float chickenSpawnX = -8.2f;
	float chickenSpawnY = 4;
	float money = 0;
	float startTime;
	float currentTime;
	Boolean initialSpawn = true;
	Coroutine chickenSpawner;

	void Start() {
		chickenSpawner = StartCoroutine(SpawnChickenContinuously());
		money = initialMoney;
		startTime = Time.time;
		UpdateUI();
	}

	private void Update() {
		currentTime = Time.time - startTime;
		TimeSpan time = TimeSpan.FromSeconds(currentTime);
		timerText.text = time.ToString("mm':'ss");
	}

	private void UpdateUI() {
		chickenText.text = chickensQty.ToString();
		coinText.text = "$ " + money.ToString();
	}

	IEnumerator SpawnChickenContinuously() {
		while (true) {
			int chickenToSpawn = 1;
			if (initialSpawn) {
				chickenToSpawn = initialChickenQty;
				initialSpawn = false;
			}
			for (int i = 0; i < chickenToSpawn; i++) {
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
			}
			yield return new WaitForSeconds(secToSpawnChicken);
		}
	}

	public void SellEgg() {
		money += moneyPerEgg;
		UpdateUI();
	}

	public void ChickenEating() {
		money -= foodPrice;
		UpdateUI();
		if (money < 0) {
			FindObjectOfType<SceneLoader>().LoadNextScene();
		}
	}
}

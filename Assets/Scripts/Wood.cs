using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{

	public static Wood Instance { get; private set; }


	[System.Serializable]
	private class RotationElement
	{
		public float speed;
		public int direction;
		public float duration;
		public int state;
		public bool isSimpleRotation;
	}


	[SerializeField]
	private RotationElement[] rotationPatterns;

	[SerializeField]
	private GameObject[] woodParticles;
	private int rotationPatternIndex = 0;

	public static GameObject spawnedWood;

	private void Awake()
	{
		Instance = this;
	}

	public static void CreateWood()
	{
		if (GameController.SpawnApple())
		{
			spawnedWood = Instantiate(WoodTuner.Instance.woodPrefabsWithApples[Variables.woodLevelIndex].woodObject, Variables.Instance.spawnWoodPosition);
		}
		else
		{
			spawnedWood = Instantiate(WoodTuner.Instance.woodPrefabsWithoutApples[Variables.woodLevelIndex].woodObject, Variables.Instance.spawnWoodPosition);
		}
	}

	public static void CreateBoss()
	{
		spawnedWood = Instantiate(WoodTuner.Instance.bossPrefabs[Variables.woodLevelIndex].woodObject, Variables.Instance.spawnWoodPosition);
	}


	private void FixedUpdate()
	{
		if (Variables._isGameStarted)
		{
			if (rotationPatterns[rotationPatternIndex].isSimpleRotation)
			{
				transform.Rotate(0, 0, rotationPatterns[rotationPatternIndex].direction * rotationPatterns[rotationPatternIndex].speed);
			}
			else
			{
				transform.Rotate(0, 0, rotationPatterns[rotationPatternIndex].direction * rotationPatterns[rotationPatternIndex].speed * rotationPatterns[rotationPatternIndex].duration);
				rotationPatterns[rotationPatternIndex].duration -= Time.fixedDeltaTime * rotationPatterns[rotationPatternIndex].state;
				if (rotationPatterns[rotationPatternIndex].duration < Random.Range(-4, 0) || rotationPatterns[rotationPatternIndex].duration > Random.Range(rotationPatterns[rotationPatternIndex].duration, 4))
				{
					rotationPatterns[rotationPatternIndex].state *= -1;
				}
			}
		}
	}


	public static void WoodDestroyed()
	{
		GameObject tempGameObject = GameObject.FindGameObjectWithTag("Wood");
		Transform tempTransform = tempGameObject.GetComponent<Transform>();
		Destroy(tempGameObject);

		foreach (GameObject particle in Instance.woodParticles)
		{
			GameObject woodParticle = Instantiate(particle, new Vector2(tempTransform.position.x + Random.Range(0.10f, 0.33f), tempTransform.position.y + Random.Range(0.10f, 0.33f)), Quaternion.identity);
			Rigidbody2D tempRigidbody = woodParticle.GetComponent<Rigidbody2D>();
			tempRigidbody.AddForce(new Vector2(Random.Range(-2, 3), Random.Range(-2, 3)) * Random.Range(1, 3), ForceMode2D.Impulse);
			var impulse = (Random.Range(-150, 151) * Mathf.Deg2Rad) * tempRigidbody.inertia;
			tempRigidbody.AddTorque(impulse, ForceMode2D.Impulse);
			Destroy(woodParticle, 3f);
		}

		if (Variables.stage % 5 == 0)
			Shop.GetNewSkin(Variables.woodLevelIndex);
	}

	public static void CompleteStage()
	{
		int temp = Variables.woodLevelIndex;

		Variables.stage++;
		GameUI.UpdateStageText(Variables.stage);
		DataManager.SaveHighStage(Variables.stage);

		if (Variables.stage % 5 == 0)
		{
			Variables.woodLevelIndex = Random.Range(1, WoodTuner.Instance.bossPrefabs.Length);
			CreateBoss();
		}
		else
		{
			while (temp == Variables.woodLevelIndex)
			{
				Variables.woodLevelIndex = Random.Range(1, WoodTuner.Instance.woodPrefabsWithoutApples.Length);
			}
			CreateWood();
		}

		GameController.Instance.SpawnKnifeSWithoutDelay();

		GameUI.Instance.DestroyDisplayedKnifeCount();
		GameUI.Instance.SetInitialDisplayedKnifeCount(WoodTuner.Instance.woodPrefabsWithoutApples[Variables.woodLevelIndex].knifeCount);

		Variables._isGameWon = false;
	}

}

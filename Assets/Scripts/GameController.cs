using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
	public static GameController Instance { get; private set; }
	public GameUI gameUI { get; private set; }

	[SerializeField]
	private ChanceTuner appleChance;
	private int knifeCount;

	[Header("Knife Settings")]
	public GameObject knifeObject;
	[SerializeField]
	private Transform knifeSpawnPosition;
	[SerializeField]
	private float knifeSpawnDelay;

	[SerializeField]
	private float force;
	private bool throwed;
	private GameObject knife;

	private void Awake()
	{
		Instance = this;
		gameUI = GetComponent<GameUI>();

		Vibration.Init();
	}

	private void Start()
	{
		Application.targetFrameRate = 60;

		knifeCount = WoodTuner.Instance.woodPrefabsWithoutApples[Variables.woodLevelIndex].knifeCount;
		gameUI.SetInitialDisplayedKnifeCount(knifeCount);
	}

	public void SpawnKnife()
	{
		StartCoroutine(SpawnDelay(knifeSpawnDelay));
	}

	public void SpawnKnifeSWithoutDelay()
	{
		throwed = false;
		knife = Instantiate(knifeObject, knifeSpawnPosition);
	}

	public void DestroyKnife()
	{
		if (knife != null)
			Destroy(knife);
	}

	public static bool SpawnApple()
	{
		if (Random.Range(0.0f, 1.0f) < Instance.appleChance.GetChance())
			return true;
		else
			return false;
	}

	public void StartGameOverSequence(bool win)
	{
		StartCoroutine(GameOverSequenceCoroutine(win));
	}

	private IEnumerator GameOverSequenceCoroutine(bool win)
	{
		if (win)
		{
			Vibration.Vibrate(100);
			Wood.WoodDestroyed();
			yield return new WaitForSecondsRealtime(0.5f);

			Wood.CompleteStage();
			knifeCount = WoodTuner.Instance.woodPrefabsWithoutApples[Variables.woodLevelIndex].knifeCount;

			gameUI.knifeIconIndexToChange = 0;
		}
		else
		{
			Vibration.Vibrate(150);
			yield return new WaitForSecondsRealtime(0.5f);

			GameUI.LaunchGameOver();
		}
	}

	private IEnumerator SpawnDelay(float delay)
	{
		yield return new WaitForSecondsRealtime(delay);
		knife = Instantiate(knifeObject, knifeSpawnPosition);
		throwed = false;
	}

	public void OnSuccessfullKnifeHit()
	{
		SoundManager.Instance.PLayHitSound();

		knifeCount--;
		gameUI.DecrementDisplayedKnifeCount();
		if (knifeCount > 0)
		{
			SpawnKnife();
		}
		else
		{
			Variables._isGameWon = true;
			StartGameOverSequence(Variables._isGameWon);
		}
	}

	public void StartGame()
	{
		Variables._isGameWon = false;
		Variables._isGameStarted = true;
		GameUI.UpdateScoreText(Variables.score);
		GameUI.UpdateStageText(Variables.stage);
		GameController.Instance.SpawnKnifeSWithoutDelay();
		Wood.CreateWood();
	}

	public void RestartGame()
	{
		Destroy(Wood.spawnedWood);
		gameUI.DestroyDisplayedKnifeCount();
		Variables.score = 0;
		Variables.stage = 1;
		Variables.woodLevelIndex = 0;
		Variables._isGameWon = false;
		Variables._isGameStarted = true;
		knifeCount = WoodTuner.Instance.woodPrefabsWithoutApples[Variables.woodLevelIndex].knifeCount;
		GameUI.UpdateScoreText(Variables.score);
		GameUI.UpdateStageText(Variables.stage);
		GameUI.Instance.knifeIconIndexToChange = 0;
		gameUI.SetInitialDisplayedKnifeCount(knifeCount);
	}

	private void ThrowKnife()
	{
		knife.transform.parent = null;
		knife.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
		throwed = true;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Variables._isGameStarted && !Variables._isGameWon && !throwed)
		{
			ThrowKnife();
		}
	}
}
